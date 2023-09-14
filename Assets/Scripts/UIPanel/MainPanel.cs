using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine.Events;
using System;
using DG.Tweening;
using System.Threading;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public class MainPanelData : UIPanelData
    {
    }

    public partial class MainPanel : UIPanel
    {
        public Image imgBk;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as MainPanelData ?? new MainPanelData();

            CancellationToken token = this.GetCancellationTokenOnDestroy();

            btnStart.AddAwaitAction(async () =>
            {
                await objStart.HideAsync();
                await ButtonGroup.ShowAsync();
                tmpModuleName.gameObject.SetActive(true);
            });

            btnPreview.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.WaitUntil(() => !ButtonGroup.gameObject.activeInHierarchy);
                await UIKit.GetPanel<PreviewPanel>().ShowAsyncPanel();
            });

            btnKnowledge.AddAwaitAction(async () =>
            {
                await ButtonGroup.HideAsync();
                await SecondButtonGroup.ShowAsync();
            });

            btnMaterial.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
                KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
                knowledgePanel.objMaterial.Show();
                await knowledgePanel.ShowAsyncPanel();
            });

            btnTool.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
                KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
                knowledgePanel.objTool.Show();
                await knowledgePanel.ShowAsyncPanel();
            });

            btnLearn.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.Delay(Settings.HideDelay);
                await UIKit.GetPanel<LearnPanel>().ShowAsyncPanel();
            });

            btninteraction.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.Delay(Settings.HideDelay);
                await UIKit.GetPanel<InteractionPanel>().ShowAsyncPanel();
            });

            btnReport.AddAwaitAction(async () =>
            {
                Hide();
                await UniTask.Delay(Settings.HideDelay);
                await UIKit.GetPanel<ReportPanel>().ShowAsyncPanel();
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

        public override async void Show()
        {
            base.Show();
            ButtonGroup.gameObject.SetActive(true);
            ButtonGroup.DOLocalMoveY(0, 0.5f);
            tmpModuleName.gameObject.SetActive(true);
        }

        public override async void Hide()
        {
            tmpModuleName.gameObject.SetActive(false);
            if (ButtonGroup.gameObject.activeInHierarchy)
                ButtonGroup.DOLocalMoveY(1080, 0.5f);
            else if (SecondButtonGroup.gameObject.activeInHierarchy)
                SecondButtonGroup.DOLocalMoveY(1080, 0.5f);
            await UniTask.Delay(Settings.ShowDelay);
            ButtonGroup.gameObject.SetActive(false);
            SecondButtonGroup.gameObject.SetActive(false);
        }
    }
}