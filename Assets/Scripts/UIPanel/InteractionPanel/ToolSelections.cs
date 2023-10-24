/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.Serialization;

namespace ZGMSXY_MYCXGY
{
    public enum ToolName
    {
        [Description("尺子")]
        Ruler,
        [Description("笔")]
        Pencil,
        [Description("钻铣床")]
        DrillAndMillingMachine,
        [Description("细头钻")]
        Bit_Fine,
        [Description("粗头钻")]
        Bit_Coarse,
        [Description("带锯")]
        Bandcut,
        [Description("打磨机")]
        Sander,
        [Description("细锉刀")]
        File_File,
        [Description("粗锉刀")]
        File_Coarse,
        [Description("打磨棒")]
        SanderStick
    }
    public partial class ToolSelections : UIElement
    {
        public List<Toggle> _toggles;
        public List<GameObject> _selectedObjs;

        private Dictionary<ToolName, int> canClickDic = new Dictionary<ToolName, int>()
        {
            { ToolName.Ruler, 2 },
            { ToolName.Pencil, 2 },
            { ToolName.DrillAndMillingMachine, 4 },
            { ToolName.Bit_Fine, 4 },
            { ToolName.Bit_Coarse, 2 },
            { ToolName.Bandcut, 5 },
            { ToolName.Sander, 3 },
            { ToolName.File_File, 1 },
            { ToolName.File_Coarse, 1 },
            { ToolName.SanderStick, 2 }
        };

        public Dictionary<ToolName, int> clickDic = new Dictionary<ToolName, int>();
        private CancellationToken _tokenDestroy;

        private void Awake()
        {
            _tokenDestroy = this.GetCancellationTokenOnDestroy();

            foreach (var VARIABLE in canClickDic)
                clickDic[VARIABLE.Key] = VARIABLE.Value;

            for (int i = 0; i < _toggles.Count; i++)
            {
                int index = i;
                _toggles[i].AddAwaitAction(isOn =>
                {
                    _selectedObjs[index].gameObject.SetActive(isOn);
                    // LayoutRebuilder.ForceRebuildLayoutImmediate(SeletedToolParent);
                });
            }
        }

        protected override void OnBeforeDestroy()
        {
        }

        public override void Show()
        {
            base.Show();
            hsTool.ResetState();
            _selectedObjs.ForEach(obj => obj.SetActive(false));
        }

        public override void Hide()
        {
            canClickDic.ForEach(pair => clickDic[pair.Key] = pair.Value);
            for (int i = 0; i < _toggles.Count; i++)
            {
                _toggles[i].SetIsOnWithoutNotify(false);
                _toggles[i].gameObject.SetActive(true);
                _selectedObjs[i].SetActive(false);
            }

            base.Hide();
        }

        async UniTask ResetToolState()
        {
			_selectedObjs.ForEach(obj => obj.SetActive(false));
            foreach (Toggle toggle in _toggles)
            {
                toggle.isOn = false;
            }
			await SelectedTool.HideAwait();
        }


        public async UniTask WaitSelectCorrectTool(CancellationToken token, params ToolName[] keyList)
        {
            if (_tokenDestroy.IsCancellationRequested || keyList == null || keyList.Length == 0)
                return;
            SelectedTool.ShowSync();
            await this.ShowAsync();

            //_toogles全部可交互
            _toggles.ForEach(tog => tog.interactable = true);

            List<Toggle> nowToggles = new List<Toggle>();
            foreach (var VARIABLE in keyList.Select(key => (int)key))
            {
                Toggle tog = _toggles[VARIABLE];
                if (!tog.gameObject.activeInHierarchy)
                    Debug.LogWarning($"{VARIABLE.ToString()}已经被关闭！");
                else
                    nowToggles.Add(tog);
            }

            while (!token.IsCancellationRequested)
            {
                await btnConfirmTool.OnClickAsync(token);
                if (nowToggles.All(tog => tog.isOn) && _toggles.FindAll(tog => tog.isOn).Count == nowToggles.Count)
                {
                    tmpRightTip.gameObject.SetActive(false);
					break;
				}
				else
                {
                    tmpRightTip.gameObject.SetActive(true);
                    tmpRightTip.text = $"选择错误！正确答案为：{keyList.Descriptions()}";
                }
            }

			_toggles.ForEach(tog => tog.interactable = false);
			await ResetToolState();
            foreach (var key in keyList)
            {
                clickDic[key] -= 1;
                if (clickDic[key] < 1)
                {
                    _toggles[((int)key)].gameObject.SetActive(false);
                }
            }
        }
    }
}