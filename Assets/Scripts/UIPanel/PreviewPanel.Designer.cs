using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:29579a04-71b0-48bc-bd73-ea202934ec21
	public partial class PreviewPanel
	{
		public const string Name = "PreviewPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public UnityEngine.UI.Toggle togTarget;
		[SerializeField]
		public UnityEngine.UI.Toggle togContent;
		[SerializeField]
		public UnityEngine.UI.ScrollRect imgRightBk;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpContent;
		[SerializeField]
		public RectTransform vlgImage;
		
		private PreviewPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			togTarget = null;
			togContent = null;
			imgRightBk = null;
			tmpContent = null;
			vlgImage = null;
			
			mData = null;
		}
		
		public PreviewPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		PreviewPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new PreviewPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
