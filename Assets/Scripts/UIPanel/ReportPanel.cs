using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace ZGMSXY_MYCXGY
{
	public class ReportPanelData : UIPanelData
	{
	}
	public partial class ReportPanel : UIPanel
	{
		[SerializeField] private List<ReportItem> reportItems;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ReportPanelData ?? new ReportPanelData();
			CancellationToken token = this.GetCancellationTokenOnDestroy();
			btnBack.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
			 {
				 Hide();
				 await UniTask.Delay(Settings.HideDelay);
				 UIKit.ShowPanel<MainPanel>();
			 }, token));
			btnSubmit.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
			{
				Hide();
				await UniTask.Delay(Settings.HideDelay);
				UIKit.ShowPanel<MainPanel>();
			}, token));

			ReportData data = new ReportData()
			{
				reportName = "名称",
				startTime = default(DateTime),
				endTime = default(DateTime),
				score = 100
			};
			foreach (var reportItem in reportItems)
			{
				reportItem.LoadData(data);
			}
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
			transform.DOLocalMoveY(0, 0.5f);
		}

		public override async void Hide()
		{
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(Settings.HideDelay);
			base.Hide();
		}

		public void LoadReport(ReportData data)
		{
			ReportItem reportItem = reportItems.Find(r => r.name == data.reportName);
			reportItem.LoadData(data);
		}
	}
}
