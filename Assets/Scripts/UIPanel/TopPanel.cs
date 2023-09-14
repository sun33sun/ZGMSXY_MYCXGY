using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;
using UnityEngine.EventSystems;

namespace ZGMSXY_MYCXGY
{
    public class TopPanelData : UIPanelData
    {
    }

    public partial class TopPanel : UIPanel
    {
        [SerializeField] private List<Toggle> togMaterials;
        private string selectedMaterialName = "";
        
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as TopPanelData ?? new TopPanelData();

            btnHelp.AddAwaitAction(async () =>await imgHelp.ShowAsync());

            btnCloseHelp.AddAwaitAction(async () =>await imgHelp.HideAsync());
            
            Func<bool> screenAnimEndFunc = Settings.GetButtonAnimatorEndFunc(btnScreen.GetComponent<Button>());
            btnScreen.OnPointerClickEvent(async d =>
            {
                UIRoot.Instance.GraphicRaycaster.enabled = false;
                await UniTask.WaitUntil(screenAnimEndFunc);
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    btnScreen.GetComponentInChildren<TextMeshProUGUI>().text = "退出全屏";
                }
                else
                {
                    btnScreen.GetComponentInChildren<TextMeshProUGUI>().text = "全屏";
                }

                UIRoot.Instance.GraphicRaycaster.enabled = true;
            });

            Vector3 tipOriginPos = imgTip.transform.localPosition;
            btnTip.AddAwaitAction(async () =>
            {
                if (imgTip.transform.localScale.x == 0)
                {
                    imgTip.transform.DOScale(Vector3.one, ExtensionFunction.ShowTime);
                    await imgTip.transform.DOLocalMove(tipOriginPos, ExtensionFunction.ShowTime).AsyncWaitForCompletion();
                }
                else
                {
                    imgTip.transform.DOScale(Vector3.zero, ExtensionFunction.HideTime);
                    await imgTip.transform.DOLocalMove(btnTip.transform.localPosition, ExtensionFunction.HideTime)
                        .AsyncWaitForCompletion();
                }
            });

            btnRetractTip.AddAwaitAction(async () =>
            {
                imgTip.transform.DOScale(Vector3.zero, ExtensionFunction.ShowTime);
                await imgTip.transform.DOLocalMove(btnTip.transform.localPosition, ExtensionFunction.ShowTime).AsyncWaitForCompletion();
            });
        }

        public async UniTask<string> WaitMaterial()
        {
            if (this.GetCancellationTokenOnDestroy().IsCancellationRequested)
                return null;
            await imgSelectMaterial.ShowAsync();
            await btnConfirmMaterial.OnClickAsync();
            return selectedMaterialName;
        }
        
        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            imgHelp.HideSync();
            imgTip.transform.localPosition = btnTip.transform.localPosition;
            imgTip.transform.localScale = Vector3.zero;
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}