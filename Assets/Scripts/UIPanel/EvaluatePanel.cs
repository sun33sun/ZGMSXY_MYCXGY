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
                await this.HideAsync();
                await UIKit.GetPanel<MainPanel>().ShowAsync();
            });

            for (int i = 0; i < togOtherModels.Count; i++)
            {
                int index = i;
                Image togImg = togOtherModels[i].GetComponent<Image>();
                Image imgOtherModel = togOtherModels[i].transform.GetChild(0).GetComponent<Image>();
                togOtherModels[i].onValueChanged.AddListener(isOn =>
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

                btnOtherModel_Selecteds[i].onClick.AddListener(async () =>
                {
                    await OtherModel.HideAsync();
                    togOtherModels[index].isOn = false;
                });
            }
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            UIKit.GetPanel<TopPanel>().tmpTip.text = "查看实验评价";
            
            for (int i = 0; i < togOtherModels.Count; i++)
            {
                if(togOtherModels[i].isOn)
                    togOtherModels[i].isOn = false;
            }
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}