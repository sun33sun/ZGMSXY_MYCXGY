using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:a0706a31-f3ac-45e3-87fc-433cd1bb3343
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
		
		private TopPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			btnHelp = null;
			btnScreen = null;
			imgHelp = null;
			btnCloseHelp = null;
			
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
