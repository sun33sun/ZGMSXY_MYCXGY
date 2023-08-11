using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;

namespace ZGMSXY_MYCXGY
{
	public class LearnPanelData : UIPanelData
	{
	}
	public partial class LearnPanel : UIPanel
	{
		[SerializeField] List<Toggle> craftItems;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as LearnPanelData ?? new LearnPanelData();

			GameObject topMask = UIKit.GetPanel<TopPanel>().imgMask.gameObject;
			CancellationToken token = this.GetCancellationTokenOnDestroy();

			Vector2 bigSize = new Vector2(240, 240);
			Vector2 smallSize = new Vector2(190, 190);
			for (int i = 0; i < craftItems.Count; i++)
			{
				int index = i;
				Func<bool> principleItemsFunc = Settings.GetToggleAnimatorEndFunc(craftItems[i]);
				RectTransform rect = craftItems[i].transform as RectTransform;
				craftItems[i].onValueChanged.AddListener(async isOn =>
				{
					if (token.IsCancellationRequested) return;
					if (isOn)
					{
						topMask.SetActive(true);
						await UniTask.WaitUntil(principleItemsFunc);
						await UniTask.Delay(500);
						rect.DOSizeDelta(bigSize, 0.5f);
						await UniTask.Delay(600);
						topMask.SetActive(false);
						LayoutRebuilder.ForceRebuildLayoutImmediate(hlgCraft);
					}
					else
					{
						rect.DOSizeDelta(smallSize, 0.5f);
						await UniTask.Delay(600);
					}
				});
			}

			btnConfirmCraft.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmCraft, async () =>
			 {
				 imgPlayEnd.gameObject.SetActive(true);
				 imgPlayEnd.DOLocalMoveY(0, 0.5f);
				 await UniTask.Delay(500);
			 }, token));
			
			btnPlayEnd.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnPlayEnd, async () =>
			{
				imgPlayEnd.DOLocalMoveY(1080, 0.5f);
				objCraft.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(600);
				titleGroup.gameObject.SetActive(true);
				titleGroup.DOLocalMoveY(0, 0.5f);
			}, token));

			btnSubmitTitle.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnSubmitTitle, async () =>
			{
				imgPlay.gameObject.SetActive(true);
				imgPlay.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(500);
			}, token));

			btnConfirmPlay.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmPlay, async () =>
			{
				Hide();
				await UniTask.WaitUntil(() => !gameObject.activeInHierarchy);
				UIKit.GetPanel<MainPanel>().Show();
			}, token));

			btnCancelPlay.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCancelPlay, async () =>
			{
				imgPlay.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(500);
				imgPlay.gameObject.SetActive(false);
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

		public override void Show()
		{
			base.Show();
			Vector3 hidePos = new Vector3(0, 1080, 0);

			objCraft.gameObject.SetActive(true);
			objCraft.localPosition = Vector3.zero;
			imgPlayEnd.gameObject.SetActive(false);
			imgPlayEnd.localPosition = hidePos;
			titleGroup.gameObject.SetActive(false);
			titleGroup.localPosition = hidePos;
			imgPlay.gameObject.SetActive(false);
			imgPlay.localPosition = hidePos;

			transform.DOLocalMoveY(0, 0.5f);
		}

		public override async void Hide()
		{
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(500);
			base.Hide();
		}
	}
}
