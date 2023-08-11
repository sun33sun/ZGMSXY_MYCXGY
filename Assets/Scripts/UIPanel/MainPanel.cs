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
			
			CheckButtonClick(btnStart, async () =>
			 {
				 objStart.DOLocalMoveY(1080, 0.5f);
				 await UniTask.Delay(600);
				 objStart.gameObject.SetActive(false);
				 ButtonGroup.gameObject.SetActive(true);
				 ButtonGroup.DOLocalMoveY(0, 0.5f);
				 tmpModuleName.gameObject.SetActive(true);
			 }).Forget();
			CheckButtonClick(btnKnowledge, async () =>
			{
				ButtonGroup.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(600);
				ButtonGroup.gameObject.SetActive(false);
				SecondButtonGroup.gameObject.SetActive(true);
				SecondButtonGroup.DOLocalMoveY(0, 0.5f);
			}).Forget();
			CheckButtonClick(btnMaterial, async () =>
			{
				Hide();
				await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
				KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
				knowledgePanel.objMaterial.Show();
				knowledgePanel.Show();
			}).Forget();
			CheckButtonClick(btnTool, async () =>
			{
				Hide();
				await UniTask.WaitUntil(() => !SecondButtonGroup.gameObject.activeInHierarchy);
				KnowledgePanel knowledgePanel = UIKit.GetPanel<KnowledgePanel>();
				knowledgePanel.objTool.Show();
				knowledgePanel.Show();
			}).Forget();

			btnLearn.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLearn, async () =>
			 {
				 Hide();
				 await UniTask.Delay(600);
				 UIKit.ShowPanel<LearnPanel>();
			 }, token));
		}

		async UniTaskVoid CheckButtonClick(Button btn, Func<UniTask> invoke)
		{
			var asyncEnumerable = btn.OnClickAsAsyncEnumerable();
			var token = this.GetCancellationTokenOnDestroy();
			Animator animator = btn.animator;
			Func<bool> func = () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
			await asyncEnumerable.ForEachAwaitAsync(async _ =>
			{
				if (token.IsCancellationRequested) return;
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.WaitUntil(func);
				await invoke();
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			}, token);
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
			await UniTask.Delay(600);
		}

		public override async void Hide()
		{
			tmpModuleName.gameObject.SetActive(false);
			if (ButtonGroup.gameObject.activeInHierarchy)
				ButtonGroup.DOLocalMoveY(1080, 0.5f);
			else if (SecondButtonGroup.gameObject.activeInHierarchy)
				SecondButtonGroup.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(600);
			ButtonGroup.gameObject.SetActive(false);
			SecondButtonGroup.gameObject.SetActive(false);
		}
	}
}
