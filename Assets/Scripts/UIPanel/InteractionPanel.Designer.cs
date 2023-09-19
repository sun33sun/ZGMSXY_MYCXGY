using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:30d37d6e-1b48-489e-a254-c87eb8a2bb1b
	public partial class InteractionPanel
	{
		public const string Name = "InteractionPanel";
		
		[SerializeField]
		public UnityEngine.Video.VideoPlayer vpRealVideo;
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public ToolSelections toolSelections;
		[SerializeField]
		public TaskSchedule taskSchedule;
		[SerializeField]
		public SelectMaterial SelectMaterial;
		[SerializeField]
		public SelectCase SelectCase;
		[SerializeField]
		public UnityEngine.RectTransform imgNext;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmNext;
		[SerializeField]
		public UnityEngine.UI.Button btnEnterNext;
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
		public UnityEngine.UI.Button btnEnterWorksLibrary;
		[SerializeField]
		public LastMaterialSelect LastMaterialSelect;
		
		private InteractionPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			vpRealVideo = null;
			btnBack = null;
			toolSelections = null;
			taskSchedule = null;
			SelectMaterial = null;
			SelectCase = null;
			imgNext = null;
			btnConfirmNext = null;
			btnEnterNext = null;
			imgPlayRealVideo = null;
			btnConfirmPlayRealVideo = null;
			btnCancelPlayRealVideo = null;
			imgSubmitModel = null;
			btnConfirmSubmitModel = null;
			btnCancelSubmitModel = null;
			btnEnterWorksLibrary = null;
			LastMaterialSelect = null;
			
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
