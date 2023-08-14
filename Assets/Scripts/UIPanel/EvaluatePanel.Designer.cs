using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:d3e54b81-fbb4-4c99-bc87-aa19fad372cc
	public partial class EvaluatePanel
	{
		public const string Name = "EvaluatePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public UnityEngine.RectTransform svSelfEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnNext;
		[SerializeField]
		public UnityEngine.RectTransform OtherModel;
		[SerializeField]
		public UnityEngine.RectTransform svDoEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnBackMain;
		[SerializeField]
		public UnityEngine.UI.Button btnEnterEvaluate;
		
		private EvaluatePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			svSelfEvaluate = null;
			btnNext = null;
			OtherModel = null;
			svDoEvaluate = null;
			btnBackMain = null;
			btnEnterEvaluate = null;
			
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
