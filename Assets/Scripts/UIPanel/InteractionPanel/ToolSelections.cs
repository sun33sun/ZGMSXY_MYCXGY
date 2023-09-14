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
using TMPro;
using UnityEngine.Serialization;

namespace ZGMSXY_MYCXGY
{
    public partial class ToolSelections : UIElement
    {
        public enum ToolType
        {
            Ruler,
            Pencil,
            DrillAndMillingMachine,
            Bit_Fine,
            Bit_Coarse,
            Bandcut,
            Sander,
            File_File,
            File_Coarse
        }

        public List<Button> _buttons;
        private Dictionary<ToolType, int> canClickDic = new Dictionary<ToolType, int>()
        {
            { ToolType.Ruler ,1},
            { ToolType.Pencil ,1},
            { ToolType.DrillAndMillingMachine ,1},
            { ToolType.Bit_Fine ,1},
            { ToolType.Bit_Coarse ,1},
            { ToolType.Bandcut ,1},
            { ToolType.Sander ,1},
            { ToolType.File_File ,1},
            { ToolType.File_Coarse ,1}            
        };
        public Dictionary<ToolType, int> clickDic = new Dictionary<ToolType, int>();
        private CancellationToken _tokenDestroy;
        
        private void Awake()
        {
            _tokenDestroy = this.GetCancellationTokenOnDestroy();
            
            foreach (var VARIABLE in canClickDic)
                clickDic[VARIABLE.Key] = VARIABLE.Value;
            
            float maxDistance = ((_buttons.Count - 5) / 2) * 140;
            btnLeftTool.AddAwaitAction(async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX > -maxDistance)
                {
                    Content.DOLocalMoveX(nowX - 140, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            });
            btnRightTool.AddAwaitAction(async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX < maxDistance)
                {
                    Content.DOLocalMoveX(nowX + 140, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            });
        }

        void InitClickDic()
        {
            
        }

        protected override void OnBeforeDestroy()
        {
        }

        public override void Hide()
        {
            foreach (var VARIABLE in canClickDic)
                clickDic[VARIABLE.Key] = VARIABLE.Value;
            _buttons.ForEach(btn => btn.gameObject.SetActive(true));
            base.Hide();
        }

        public async UniTask WaitButtonsClick(params ToolType[] keyList)
        {
            if(_tokenDestroy.IsCancellationRequested || keyList == null || keyList.Length == 0)
                return;
            
            List<UniTask> list = new List<UniTask>();
            foreach (var VARIABLE in keyList)
            {
                Button btn = _buttons[((int)VARIABLE)];
                if (!btn.gameObject.activeInHierarchy)
                {
                    Debug.LogWarning($"{VARIABLE.ToString()}已经被关闭！");
                }
                else
                {
                    list.Add(btn.OnClickAsync(_tokenDestroy));
                }
            }
            await UniTask.WhenAll(list);
            foreach (var key in keyList)
            {
                clickDic[key] -= 1;
                if (clickDic[key] == 0)
                {
                    _buttons[((int)key)].gameObject.SetActive(false);
                }
            }
        }
    }
}