using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:7f417231-0df8-45ef-86e5-3d85812bf7fd
	public partial class KnowledgePanel
	{
		public const string Name = "KnowledgePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public ObjMaterial objMaterial;
		[SerializeField]
		public ObjTool objTool;
		
		private KnowledgePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			objMaterial = null;
			objTool = null;
			
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
