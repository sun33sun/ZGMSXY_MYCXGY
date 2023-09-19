/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
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
    public partial class ToolSelections : UIElement
    {
        public enum Tool
        {
            Ruler,
            Pencil,
            DrillAndMillingMachine,
            Bit_Fine,
            Bit_Coarse,
            Bandcut,
            Sander,
            File_File,
            File_Coarse,
            SanderStick
        }

        public List<Toggle> _toggles;
        public List<GameObject> _selectedObjs;

        private Dictionary<Tool, int> canClickDic = new Dictionary<Tool, int>()
        {
            { Tool.Ruler, 2 },
            { Tool.Pencil, 2 },
            { Tool.DrillAndMillingMachine, 4 },
            { Tool.Bit_Fine, 4 },
            { Tool.Bit_Coarse, 2 },
            { Tool.Bandcut, 5 },
            { Tool.Sander, 3 },
            { Tool.File_File, 1 },
            { Tool.File_Coarse, 1 },
            { Tool.SanderStick, 2 }
        };

        public Dictionary<Tool, int> clickDic = new Dictionary<Tool, int>();
        private CancellationToken _tokenDestroy;

        private void Awake()
        {
            _tokenDestroy = this.GetCancellationTokenOnDestroy();

            foreach (var VARIABLE in canClickDic)
                clickDic[VARIABLE.Key] = VARIABLE.Value;

            float maxDistance = ((_toggles.Count - 5) / 2) * 140;
            btnLeftTool.AddAwaitAction(async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX < maxDistance)
                {
                    await Content.DOLocalMoveX(nowX + 140, 0.1f).AsyncWaitForCompletion();
                }
            });
            btnRightTool.AddAwaitAction(async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX > -maxDistance)
                {
                    await Content.DOLocalMoveX(nowX - 140, 0.1f).AsyncWaitForCompletion();
                }
            });
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
            Content.transform.localPosition = new Vector3(75, 0, 0);
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


        public async UniTask WaitSelectCorrectTool(CancellationToken token, params Tool[] keyList)
        {
            if (_tokenDestroy.IsCancellationRequested || keyList == null || keyList.Length == 0)
                return;
            SelectedTool.ShowSync();
            await this.ShowAsync();
            
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
                    break;
            }
            
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