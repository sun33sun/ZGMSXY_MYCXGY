using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using ProjectBase;

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
				await this.HideAsync();
				await UIKit.GetPanel<MainPanel>().ShowAsync();
			 }, token));
			btnSubmit.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
			{
				await this.HideAsync();
				await UIKit.GetPanel<MainPanel>().ShowAsync();
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
			UIKit.GetPanel<TopPanel>().tmpTip.text = "在该页面查看实验报告";
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		public void LoadReport(ReportData data)
		{
			ReportItem reportItem = reportItems.Find(r => r.name == data.reportName);
			reportItem.LoadData(data);
		}
	}
}
