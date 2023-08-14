using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine.Events;
using System;
using DG.Tweening;
using System.Threading;

namespace ZGMSXY_MYCXGY
{
	public class MainPanelData : UIPanelData
	{
	}
	public partial class MainPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as MainPanelData ?? new MainPanelData();

			CancellationToken token = this.GetCancellationTokenOnDestroy();

			btnStart.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnStart, async () =>
			{
				objStart.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				objStart.gameObject.SetActive(false);
				ButtonGroup.gameObject.SetActive(true);
				ButtonGroup.DOLocalMoveY(0, 0.5f);
				tmpModuleName.gameObject.SetActive(true);
			}, token));

			btnKnowledge.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnKnowledge, async () =>
			{
				ButtonGroup.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				ButtonGroup.gameObject.SetActive(false);
				SecondButtonGroup.gameObject.SetActive(true);
				SecondButtonGroup.DOLocalMoveY(0, 0.5f);
			}, token));

			btnMaterial.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnMaterial, async () =>
			{
				Hide();
				await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
				KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
				knowledgePanel.objMaterial.Show();
				knowledgePanel.Show();
			}, token));

			btnTool.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnTool, async () =>
			 {
				 Hide();
				 await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
				 KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
				 knowledgePanel.objTool.Show();
				 knowledgePanel.Show();
			 }, token));

			btnLearn.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLearn, async () =>
			 {
				 Hide();
				 await UniTask.Delay(Settings.HideDelay);
				 UIKit.ShowPanel<LearnPanel>();
			 }, token));

			btninteraction.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btninteraction, async () =>
			{
				Hide();
				await UniTask.Delay(Settings.HideDelay);
				UIKit.ShowPanel<InteractionPanel>();
			}, token));

			btnReport.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnReport, async () =>
			{
				Hide();
				await UniTask.Delay(Settings.HideDelay);
				UIKit.ShowPanel<ReportPanel>();
			}, token));
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
