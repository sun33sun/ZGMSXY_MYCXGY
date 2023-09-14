using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:aeb3dbc7-fe77-4e0f-8de4-a16dcfeedcc2
	public partial class TopPanel
	{
		public const string Name = "TopPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public UnityEngine.UI.Button btnHelp;
		[SerializeField]
		public UnityEngine.EventSystems.EventTrigger btnScreen;
		[SerializeField]
		public UnityEngine.RectTransform imgHelp;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseHelp;
		[SerializeField]
		public UnityEngine.UI.Image imgTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTip;
		[SerializeField]
		public UnityEngine.UI.Button btnRetractTip;
		[SerializeField]
		public UnityEngine.RectTransform imgSelectMaterial;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmMaterial;
		
		private TopPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			btnHelp = null;
			btnScreen = null;
			imgHelp = null;
			btnCloseHelp = null;
			imgTip = null;
			tmpTip = null;
			btnRetractTip = null;
			imgSelectMaterial = null;
			btnConfirmMaterial = null;
			
			mData = null;
		}
		
		public TopPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TopPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TopPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
