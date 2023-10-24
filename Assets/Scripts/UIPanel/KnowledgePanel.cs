using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using System.Collections.Generic;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public class KnowledgePanelData : UIPanelData
	{
	}
	public partial class KnowledgePanel : UIPanel
	{
		private DateTime _startTime;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgePanelData ?? new KnowledgePanelData();

			btnBack.AddAwaitAction(async () =>
			{
				await this.HideAsync();
				await UIKit.GetPanel<MainPanel>().ShowAsync();
			});
			
		}
		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			_startTime = DateTime.Now;
			UIKit.GetPanel<TopPanel>().tmpTip.text = "查看背景知识";
		}

		protected override void OnHide()
		{
			ReportData data = new ReportData()
			{
				reportName = "工艺基础认知",
				startTime = _startTime,
				endTime =  DateTime.Now,
				score = 1
			};
			UIKit.GetPanel<ReportPanel>().LoadReport(data);

			GameManager.Instance.gameObject.SetActive(false);
			objMaterial.Hide();
			objTool.Hide();
			objCase.Hide();
		}

		protected override void OnClose()
		{
		}
	}
}
