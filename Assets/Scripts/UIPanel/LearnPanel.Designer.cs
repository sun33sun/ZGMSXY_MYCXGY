using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:d39a7819-7af3-42b1-8b91-111ba54f86a4
	public partial class LearnPanel
	{
		public const string Name = "LearnPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnBack;
		[SerializeField]
		public RectTransform objCraft;
		[SerializeField]
		public UnityEngine.RectTransform hlgCraft;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpCraftName;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpCraftDescription;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmCraft;
		[SerializeField]
		public UnityEngine.RectTransform imgAnimation;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmAnimation;
		[SerializeField]
		public UnityEngine.RectTransform imgAnimationEnd;
		[SerializeField]
		public UnityEngine.UI.Button btnEnterExamination;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelAnimation;
		[SerializeField]
		public RectTransform titleGroup;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmTitle;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitTitle;
		[SerializeField]
		public QuestionManager Content;
		[SerializeField]
		public UnityEngine.RectTransform imgDoubleConfirmTitle;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirmTitle;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancelTitle;
		[SerializeField]
		public UnityEngine.RectTransform imgPlay;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmPlay;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelPlay;
		[SerializeField]
		public UnityEngine.Video.VideoPlayer vpVideo;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseVideo;
		
		private LearnPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			objCraft = null;
			hlgCraft = null;
			tmpCraftName = null;
			tmpCraftDescription = null;
			btnConfirmCraft = null;
			imgAnimation = null;
			btnConfirmAnimation = null;
			imgAnimationEnd = null;
			btnEnterExamination = null;
			btnCancelAnimation = null;
			titleGroup = null;
			btnConfirmTitle = null;
			btnSubmitTitle = null;
			Content = null;
			imgDoubleConfirmTitle = null;
			btnDoubleConfirmTitle = null;
			btnDoubleCancelTitle = null;
			imgPlay = null;
			btnConfirmPlay = null;
			btnCancelPlay = null;
			vpVideo = null;
			btnCloseVideo = null;
			
			mData = null;
		}
		
		public LearnPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		LearnPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new LearnPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
