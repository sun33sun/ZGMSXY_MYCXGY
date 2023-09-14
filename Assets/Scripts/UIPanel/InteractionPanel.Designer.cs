using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.Serialization;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:ae723b28-552c-46c0-8ea8-3689585ffbd7
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
		public SelectMaterial SelectMaterial;
		[SerializeField]
		public SelectCase SelectCase;
		[FormerlySerializedAs("MaterialDrilling")] [SerializeField]
		public ToolSelections toolSelections;
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
		[SerializeField]
		public UnityEngine.UI.Button btnEnterWorksLibrary;
		[SerializeField]
		public LastMaterialSelect LastMaterialSelect;
		
		private InteractionPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			vpRealVideo = null;
			btnBack = null;
			taskSchedule = null;
			SelectMaterial = null;
			SelectCase = null;
			toolSelections = null;
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
