using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:8ab452e2-0f11-4b3d-8080-38b51899baf1
	public partial class EvaluatePanel
	{
		public const string Name = "EvaluatePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public UnityEngine.RectTransform OtherModel;
		
		private EvaluatePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			OtherModel = null;
			
			mData = null;
		}
		
		public EvaluatePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		EvaluatePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new EvaluatePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
