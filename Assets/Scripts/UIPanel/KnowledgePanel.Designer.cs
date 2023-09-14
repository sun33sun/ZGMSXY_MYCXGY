using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:1f96df6e-bacf-4925-a117-7d1fdd5e7726
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
