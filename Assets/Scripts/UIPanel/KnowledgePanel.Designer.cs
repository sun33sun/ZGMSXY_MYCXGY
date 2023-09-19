using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:629de50e-8ec4-4a8d-83ee-da2efaab66f2
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
