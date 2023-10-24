using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:79e7933f-ad3a-4024-83e3-bf0e475625f3
	public partial class KnowledgePanel
	{
		public const string Name = "KnowledgePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public ObjMaterial objMaterial;
		[SerializeField]
		public ObjTool objTool;
		[SerializeField]
		public ObjCase objCase;
		
		private KnowledgePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			objMaterial = null;
			objTool = null;
			objCase = null;
			
			mData = null;
		}
		
		public KnowledgePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		KnowledgePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new KnowledgePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
