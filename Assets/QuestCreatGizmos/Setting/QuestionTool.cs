using System;
using System.Collections;
using System.Collections.Generic;
using LTYFrameWork.UI;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTool : MonoBehaviour
{
	public static Text _题目文字框_Static;

	public Transform _选项组_Static;

	public static GameObject _选项预制体_Static;

	public Text _解析文字框_Static;

	public Color _选中颜色_Static;

	public List<Toggle> _所有选项 = new List<Toggle>();
	List<UICornerText> _所有选项文字框 = new List<UICornerText>();

	public void OnEnable()
	{
		_题目文字框_Static = this.gameObject.transform.Find("题目").GetChild(0).GetComponent<UICornerText>();
		_选项组_Static = this.gameObject.transform.Find("选项组");
		_解析文字框_Static = this.gameObject.transform.Find("解析").GetChild(0).GetComponent<UICornerText>();
		_解析文字框_Static.text = "";
		_选项预制体_Static = Resources.Load<GameObject>("QuestionPrefabs/选项");
	}

	public void SetQuestion(string _题目, List<string> _选项, bool _是否为单选)
	{
		_题目文字框_Static.text = _题目.ToString() + "E";
		GameObject go = null;
		ToggleGroup group = null;
		if (_是否为单选)
		{
			group = _选项组_Static.gameObject.AddComponent<ToggleGroup>();
			group.allowSwitchOff = true;
		}
		for (int i = 0; i < _选项.Count; i++)
		{
			go = Instantiate(_选项预制体_Static, _选项组_Static);
			//选项文字
			_所有选项文字框.Add(go.transform.Find("选项内容").GetComponent<UICornerText>());
			_所有选项文字框[i].text = _选项[i].ToString();
			//选项
			_所有选项.Add(go.GetComponent<Toggle>());
			_所有选项[i].group = group;
			_所有选项[i].isOn = false;
			//添加订阅方法
			AddToggleCallBack(i);
		}
	}

	void AddToggleCallBack(int togIndex)
	{
		_所有选项[togIndex].onValueChanged.AddListener(isOn =>
		{
			if (isOn)
			{
				_所有选项[togIndex].graphic.color = _选中颜色_Static;
				_所有选项[togIndex].targetGraphic.color = _选中颜色_Static;
				_所有选项文字框[togIndex].color = _选中颜色_Static;
			}
			else
			{
				_所有选项[togIndex].graphic.color = Color.white;
				_所有选项[togIndex].targetGraphic.color = Color.white;
				_所有选项文字框[togIndex].color = Color.black;
			}
		});
	}

	public float SingleJudge(QuestionController.MyChoiceIndex _正确答案, string _解析内容, float _该题分值)
	{
		int rightIndex = _正确答案.GetHashCode();
		for (int i = 0; i < _所有选项.Count; i++)
		{
			if (_所有选项[i].isOn)
			{
				_所有选项[i].graphic.color = Color.red;
				_所有选项[i].targetGraphic.color = Color.red;
				_所有选项文字框[i].color = Color.red;
			}
			_所有选项[i].interactable = false;
		}
		_所有选项[rightIndex].graphic.color = Color.green;
		_所有选项[rightIndex].targetGraphic.color = Color.green;
		_所有选项文字框[rightIndex].color = Color.green;

		if (_所有选项[rightIndex].isOn)
		{
			_解析文字框_Static.text = "回答正确！" + _解析内容.ToString() + "E";
			_解析文字框_Static.color = Color.green;
			return _该题分值;
		}
		else
		{
			_解析文字框_Static.text = "回答错误！" + "正确答案为：" + _正确答案.ToString() + "、" + _解析内容.ToString() + "E";
			_解析文字框_Static.color = Color.red;
			return 0;
		}
	}

	public float MultipleJudge(List<QuestionController.MyChoiceIndex> _正确答案组, string _解析内容, float _该题分值)
	{
		int I = 0;
		string X = "";
		for (int i = 0; i < _所有选项.Count; i++)
		{
			//选中的选项全部变为红色
			if (_所有选项[i].isOn)
			{
				_所有选项[i].graphic.color = Color.red;
				_所有选项[i].targetGraphic.color = Color.red;
				_所有选项文字框[i].color = Color.red;
			}
			//关闭交互
			_所有选项[i].interactable = false;
		}
		for (int i = 0; i < _正确答案组.Count; i++)
		{
			//将正确选项的颜色变为绿色
			_所有选项[i].graphic.color = Color.green;
			_所有选项[i].targetGraphic.color = Color.green;
			_所有选项文字框[i].color = Color.green;
			//检查正确选项是否选中
			if (_所有选项[_正确答案组[i].GetHashCode()].isOn)
			{
				I++;
			}
			//计算解析
			X = X + _正确答案组[i].ToString() + "、";

		}
		//显示解析
		if (I == _正确答案组.Count)
		{
			_解析文字框_Static.text = "回答正确！" + _解析内容.ToString() + "E";
			_解析文字框_Static.color = Color.green;
			return _该题分值;
		}
		else
		{
			_解析文字框_Static.text = "回答错误！" + "正确答案为：" + X + _解析内容.ToString() + "E";
			_解析文字框_Static.color = Color.red;
			return 0;
		}
	}

	public void Reset()
	{
		_解析文字框_Static.text = "";

		for (int i = 0; i < _所有选项.Count; i++)
		{
			_所有选项[i].isOn = false;
			_所有选项[i].graphic.color = Color.white;
			_所有选项[i].targetGraphic.color = Color.white;
			_所有选项文字框[i].color = Color.black;
		}
	}
}