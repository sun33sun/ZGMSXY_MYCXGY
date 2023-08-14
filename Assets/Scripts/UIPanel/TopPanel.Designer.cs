using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:16936e17-c3c4-4b1b-8f97-80cc3e5ee544
	public partial class TopPanel
	{
		public const string Name = "TopPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public UnityEngine.UI.Button btnHelp;
		[SerializeField]
		public UnityEngine.UI.Button btnScreen;
		
		private TopPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			btnHelp = null;
			btnScreen = null;
			
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
