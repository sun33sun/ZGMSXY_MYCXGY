using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:f759d59e-6b90-440e-92ea-624e09bb88b9
	public partial class InteractionPanel
	{
		public const string Name = "InteractionPanel";
		
		[SerializeField]
		public UnityEngine.Video.VideoPlayer vpRealVideo;
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public TaskSchedule taskSchedule;
		[SerializeField]
		public MaterialGroup materialGroup;
		[SerializeField]
		public UnityEngine.RectTransform imgNext;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmNext;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelNext;
		[SerializeField]
		public UnityEngine.RectTransform imgPlayRealVideo;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmPlayRealVideo;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelPlayRealVideo;
		[SerializeField]
		public UnityEngine.RectTransform imgSubmitModel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmSubmitModel;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelSubmitModel;
		[SerializeField]
		public UnityEngine.UI.Button btnEnterEvaluate;
		
		private InteractionPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			vpRealVideo = null;
			btnBack = null;
			taskSchedule = null;
			materialGroup = null;
			imgNext = null;
			btnConfirmNext = null;
			btnCancelNext = null;
			imgPlayRealVideo = null;
			btnConfirmPlayRealVideo = null;
			btnCancelPlayRealVideo = null;
			imgSubmitModel = null;
			btnConfirmSubmitModel = null;
			btnCancelSubmitModel = null;
			btnEnterEvaluate = null;
			
			mData = null;
		}
		
		public InteractionPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		InteractionPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new InteractionPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
