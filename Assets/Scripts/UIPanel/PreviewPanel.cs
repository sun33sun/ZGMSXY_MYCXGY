using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
    [Serializable]
    public class PreviewContent
    {
        public string strContent;
        public List<Sprite> sprites;
    }

    public class PreviewPanelData : UIPanelData
    {
    }

    public partial class PreviewPanel : UIPanel
    {
        [Header("加载图片的预制体")] [SerializeField] private Image _imagePrefanb;

        [Header("需要加载的资源")] [SerializeField] private List<PreviewContent> _previewContents;

        private List<Image> _images = new List<Image>();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as PreviewPanelData ?? new PreviewPanelData();

            btnBack.AddAwaitAction(async () =>
            {
                await this.HideAsync();
                await UIKit.GetPanel<MainPanel>().ShowAsync();                
            });

            Func<UniTask> showFunc = imgRightBk.GetShowAsync(imgRightBk.transform.localPosition);
            togTarget.AddAwaitAction(async isOn =>
            {
                if (isOn)
                {
                    await imgRightBk.HideAsync();
                    Reload(0);
                    await showFunc();
                    //重建布局
                    await RebuildAsync();
                }
            });

            togContent.AddAwaitAction(async isOn =>
            {
                if (isOn)
                {
                    await imgRightBk.HideAsync();
                    Reload(1);
                    await showFunc();
                    //重建布局
                    await RebuildAsync();
                }
            });
        }

        void Reload(int index)
        {
            for (int i = _images.Count - 1; i > -1; i--)
            {
                Destroy(_images[i].gameObject);
            }

            _images.Clear();

            for (int i = _previewContents[index].sprites.Count - 1; i > -1; i--)
            {
                Image image = Instantiate(_imagePrefanb);
                image.sprite = _previewContents[index].sprites[i];
                image.transform.SetParent(vlgImage, false);
                _images.Add(image);
            }
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            UIKit.GetPanel<TopPanel>().tmpTip.text = "通过文字与图片说明，了解实验目的、实验原理和实验内容";
            
            togTarget.isOn = true;
            Reload(0);
            RebuildAsync().Forget();
        }

        async UniTask RebuildAsync()
        {
            //重建布局
            LayoutRebuilder.ForceRebuildLayoutImmediate(vlgImage);
            await UniTask.Yield();
            LayoutRebuilder.ForceRebuildLayoutImmediate(imgRightBk.transform as RectTransform);
            await UniTask.Yield();
            imgRightBk.verticalScrollbar.value = 1;
            await UniTask.Yield();
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}