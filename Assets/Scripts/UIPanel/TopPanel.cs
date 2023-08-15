using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.EventSystems;

namespace ZGMSXY_MYCXGY
{
    public class TopPanelData : UIPanelData
    {
    }

    public partial class TopPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as TopPanelData ?? new TopPanelData();
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            Vector2 hidePos = new Vector2(709, 480);
            Vector3 hideScale = new Vector3(0, 0, 1);
            btnHelp.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnHelp, async () =>
            {
                if (imgHelp.gameObject.activeInHierarchy)
                {
                    imgHelp.DOAnchorPos(hidePos, 0.5f);
                    imgHelp.DOScale(hideScale, 0.5f);
                    await UniTask.Delay(Settings.HideDelay);
                    imgHelp.gameObject.SetActive(false);
                }
                else                    
                {
                    imgHelp.gameObject.SetActive(true);
                    imgHelp.DOAnchorPos(Vector2.one, 0.5f);
                    imgHelp.DOScale(Vector3.one, 0.5f);
                    await UniTask.Delay(Settings.ShowDelay);
                }
            }, token));
            btnCloseHelp.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCloseHelp, async () =>
            {
                imgHelp.DOAnchorPos(hidePos, 0.5f);
                imgHelp.DOScale(hideScale, 0.5f);
                await UniTask.Delay(Settings.HideDelay);
                imgHelp.gameObject.SetActive(false);
            }, token));

            UIEventTool.AddEventTrigger(btnScreen.gameObject, EventTriggerType.PointerClick, data =>
            {
                Screen.fullScreen = !Screen.fullScreen;
                if(Screen.fullScreen)
                {
                    btnScreen.GetComponentInChildren<Text>().text = "退出全屏";
                }
                else
                {
                    btnScreen.GetComponentInChildren<Text>().text = "全屏";
                }
            });
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
    }
}