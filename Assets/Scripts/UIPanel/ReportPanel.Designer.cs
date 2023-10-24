using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:5cf82d11-4a21-41c5-9c75-67eb15c3eb43
	public partial class ReportPanel
	{
		public const string Name = "ReportPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public UnityEngine.RectTransform svReport;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDate;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalScore;
		[SerializeField]
		public UnityEngine.UI.InputField inputTestEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		
		private ReportPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			svReport = null;
			tmpDate = null;
			tmpTotalScore = null;
			inputTestEvaluate = null;
			btnSubmit = null;
			
			mData = null;
		}
		
		public ReportPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ReportPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ReportPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
