using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:f72bb580-32a0-4d27-aee3-a88d5a2bc5aa
	public partial class TopPanel
	{
		public const string Name = "TopPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public UnityEngine.UI.Button btnHelp;
		[SerializeField]
		public UnityEngine.UI.Button btnScreen;
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
