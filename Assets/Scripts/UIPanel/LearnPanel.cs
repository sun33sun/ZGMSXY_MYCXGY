using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

namespace ZGMSXY_MYCXGY
{
	public class LearnPanelData : UIPanelData
	{
	}
	public partial class LearnPanel : UIPanel
	{
		[SerializeField] List<Toggle> craftItems;
		Vector2 bigSize = new Vector2(240, 240);
		Vector2 smallSize = new Vector2(190, 190);
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as LearnPanelData ?? new LearnPanelData();

			CancellationToken token = this.GetCancellationTokenOnDestroy();

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
						UIRoot.Instance.GraphicRaycaster.enabled = false;
						await UniTask.WaitUntil(principleItemsFunc);
						rect.sizeDelta = bigSize;
						LayoutRebuilder.ForceRebuildLayoutImmediate(hlgCraft);
						UIRoot.Instance.GraphicRaycaster.enabled = true;
					}
					else
					{
						rect.sizeDelta = smallSize;
					}
				});
			}

			btnBack.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
			{
				Hide();
				await UniTask.Delay(Settings.HideDelay);
				UIKit.ShowPanel<MainPanel>();
			}, token));

			btnConfirmCraft.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmCraft, async () =>
			 {
				 imgPlayEnd.gameObject.SetActive(true);
				 imgPlayEnd.DOLocalMoveY(0, 0.5f);
				 await UniTask.Delay(Settings.HideDelay);
			 }, token));
			
			btnPlayEnd.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnPlayEnd, async () =>
			{
				imgPlayEnd.DOLocalMoveY(1080, 0.5f);
				objCraft.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				titleGroup.gameObject.SetActive(true);
				titleGroup.DOLocalMoveY(0, 0.5f);
			}, token));

			btnSubmitTitle.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnSubmitTitle, async () =>
			{
				imgPlay.gameObject.SetActive(true);
				imgPlay.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
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
				await UniTask.Delay(Settings.HideDelay);
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
			foreach (var craftItem in craftItems)
			{
				if (craftItem.isOn)
					craftItem.isOn = false;
			}
			craftItems[0].isOn = true;
			transform.DOLocalMoveY(0, 0.5f);
		}

		public override async void Hide()
		{
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(Settings.HideDelay);
			base.Hide();
		}
	}
}
