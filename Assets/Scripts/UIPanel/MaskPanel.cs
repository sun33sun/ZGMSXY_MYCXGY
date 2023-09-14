using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
    public class MaskPanelData : UIPanelData
    {
    }

    public partial class MaskPanel : UIPanel
    {
        private bool isCompleted = false;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as MaskPanelData ?? new MaskPanelData();
            TextAnimation();
        }

        async void TextAnimation()
        {
            string str = tmpStory.text;
            DOTween.To(() => String.Empty, value => tmpStory.text = value, str, 8f);
            await UniTask.Delay(8000);
            isCompleted = true;
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        public override async void Hide()
        {
            await UniTask.WaitUntil(() => isCompleted);
            await transform.DOLocalMoveY(1080, 0.5f).AsyncWaitForCompletion();
            base.Hide();
        }
    }
}