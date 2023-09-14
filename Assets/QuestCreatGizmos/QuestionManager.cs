using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    /// <summary>
    /// 作者：王朕    时间：2023年7月11日
    /// </summary>
    public Transform _考题生成位置;

    public List<float> _得分 = new List<float>();

    [Header("——角标格式:[类型|尺寸|颜色=内容]——")]
    [Header("*类型:1 上标、2 下标、3 上标带中括号、4 下标带中括号*")]
    [Header("*尺寸:默认为文本尺寸的1/2*")]
    [Header("*颜色:格式为颜色码；如FF0000FF*")]
    [Header("*内容:正常输入字符、如角标内容含有[]，则使用3 4类型*")]
    [Space(10)]
    public List<QuestionController> _考题组 = new List<QuestionController>();

    private GameObject QuestionPrefab; //没有把这个鬼东西改为自动查找预制体，目的是为了如果有多种款式的界面 可以自己选择  结果这个想法也没等到扩展

    [HideInInspector] public List<QuestionTool> QuestionToolList;
    private GameObject go;

    public void Awake()
    {
        QuestionPrefab = Resources.Load<GameObject>("QuestionPrefabs/选择题");
        for (int i = 0; i < _考题组.Count; i++)
        {
            go = Instantiate(QuestionPrefab, _考题生成位置);
            Debug.Log(go.name);
            QuestionToolList.Add(go.GetComponent<QuestionTool>());
            List<string> AllChoiceContent = new List<string>();
            foreach (var ChoiceContent in _考题组[i]._选项)
            {
                AllChoiceContent.Add(ChoiceContent.Key.ToString() + ":" + ChoiceContent.Value + "E");
            }

            if (_考题组[i]._选项类型 == global::QuestionController.MyChoice.单选)
            {
                QuestionToolList[i].SetQuestion(_考题组[i]._题目.ToString(), AllChoiceContent, true);
            }
            else
            {
                QuestionToolList[i].SetQuestion(_考题组[i]._题目.ToString(), AllChoiceContent, false);
            }
        }
    }

    public void Submit()
    {
        for (int i = 0; i < QuestionToolList.Count; i++)
        {
            if (_考题组[i]._解析内容 == null)
            {
                _考题组[i]._解析内容 = "";
            }

            if (_考题组[i]._选项类型 == global::QuestionController.MyChoice.单选)
            {
                _得分.Add(QuestionToolList[i].SingleJudge(_考题组[i]._单选或判断正确答案, _考题组[i]._解析内容.ToString(), _考题组[i]._分值));
            }
            else
            {
                _得分.Add(QuestionToolList[i].MultipleJudge(_考题组[i]._多选正确答案, _考题组[i]._解析内容.ToString(), _考题组[i]._分值));
            }
        }
    }

    private void OnEnable()
    {
        foreach (var VARIABLE in QuestionToolList)
        {
            VARIABLE.Reset();
        }
    }
}