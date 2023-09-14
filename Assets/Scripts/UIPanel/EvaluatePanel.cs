using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public class EvaluatePanelData : UIPanelData
    {
    }

    public partial class EvaluatePanel : UIPanel
    {
        [SerializeField] List<Toggle> togOtherModels;
        [SerializeField] List<Button> btnOtherModel_Selecteds;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as EvaluatePanelData ?? new EvaluatePanelData();

            CancellationToken token = this.GetCancellationTokenOnDestroy();

            btnBack.AddAwaitAction(async () =>
            {
                await this.HideAsyncPanel();
                await UIKit.GetPanel<MainPanel>().ShowAsyncPanel();
            });

            btnNext.AddAwaitAction(async () =>
            {
                await svSelfEvaluate.HideAsync();
                await OtherModel.ShowAsync();
            });

            for (int i = 0; i < togOtherModels.Count; i++)
            {
                int index = i;
                Image togImg = togOtherModels[i].GetComponent<Image>();
                Image imgOtherModel = togOtherModels[i].transform.GetChild(0).GetComponent<Image>();
                togOtherModels[i].AddAwaitAction(isOn =>
                {
                    if (isOn)
                    {
                        togImg.color = new Color(0.9f, 0.58f, 0.22f, 1);
                        imgOtherModel.color = new Color(0.5f, 0.5f, 0.5f, 1);
                        btnOtherModel_Selecteds[index].gameObject.SetActive(true);
                    }
                    else
                    {
                        togImg.color = Color.white;
                        imgOtherModel.color = Color.white;
                        btnOtherModel_Selecteds[index].gameObject.SetActive(false);
                    }
                });

                btnOtherModel_Selecteds[i].AddAwaitAction(async () =>
                {
                    await OtherModel.HideAsync();
                    togOtherModels[index].isOn = false;
                    btnEnterEvaluate.gameObject.SetActive(true);
                });
            }

            btnEnterEvaluate.AddAwaitAction(async () =>
            {
                svDoEvaluate.gameObject.SetActive(true);
                await svDoEvaluate.DOLocalMoveY(-45, 0.5f).AsyncWaitForCompletion();
                btnEnterEvaluate.gameObject.SetActive(false);
            });

            btnBackMain.AddAwaitAction(async () =>
            {
                await this.HideAsyncPanel();
                await UIKit.GetPanel<MainPanel>().ShowAsyncPanel();
            });
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            svSelfEvaluate.gameObject.SetActive(true);
            svSelfEvaluate.localPosition = new Vector3(0, -45, 0);
            OtherModel.gameObject.SetActive(false);
            OtherModel.localPosition = new Vector3(0, 1080, 0);
            svDoEvaluate.gameObject.SetActive(false);
            svDoEvaluate.transform.localPosition = new Vector3(0, 1080, 0);
            btnEnterEvaluate.gameObject.SetActive(false);
            for (int i = 0; i < togOtherModels.Count; i++)
            {
                if(togOtherModels[i].isOn)
                    togOtherModels[i].isOn = false;
            }
            transform.DOLocalMoveY(0, 0.5f);
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}