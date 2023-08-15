using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using System.Collections.Generic;

namespace ZGMSXY_MYCXGY
{
	public class KnowledgePanelData : UIPanelData
	{
	}
	public partial class KnowledgePanel : UIPanel
	{
		[SerializeField] Transform MaterialGroup;
		[SerializeField] Transform EntityGroup;
		//[SerializeField] List<Toggle> togPrinciples;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgePanelData ?? new KnowledgePanelData();

			btnBack.onClick.AddListener(
				Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
				{
					Hide();
					await UniTask.WaitUntil(() => !gameObject.activeInHierarchy);
					UIKit.GetPanel<MainPanel>().Show();
				}, this.GetCancellationTokenOnDestroy()
			));
		}
		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			Vector3 hidePos = new Vector3(0, 1080, 0);
			MaterialGroup.localPosition = Vector3.zero;
			MaterialGroup.gameObject.SetActive(true);
			EntityGroup.localPosition = hidePos;
			EntityGroup.gameObject.SetActive(false);
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
			transform.DOLocalMoveY(0, 0.5f);
		}

		public override async void Hide()
		{
			ModelRoot.Instance.gameObject.SetActive(false);
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(Settings.HideDelay);
			objMaterial.Hide();
			objTool.Hide();
			base.Hide();
		}
	}
}
