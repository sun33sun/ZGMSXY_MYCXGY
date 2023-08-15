using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:7645ffc0-fefe-47e0-915a-38b9c571e783
	public partial class MaskPanel
	{
		public const string Name = "MaskPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpStory;
		
		private MaskPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			tmpStory = null;
			
			mData = null;
		}
		
		public MaskPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		MaskPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MaskPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
