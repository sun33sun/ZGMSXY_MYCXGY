using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:23d56d5c-7ab2-4232-8fa5-8325e9d5c824
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
		public UnityEngine.UI.Button btnConfirmCraft;
		[SerializeField]
		public UnityEngine.RectTransform imgPlayEnd;
		[SerializeField]
		public UnityEngine.UI.Button btnPlayEnd;
		[SerializeField]
		public RectTransform titleGroup;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitTitle;
		[SerializeField]
		public QuestionManager Content;
		[SerializeField]
		public UnityEngine.RectTransform imgPlay;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmPlay;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelPlay;
		
		private LearnPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnBack = null;
			objCraft = null;
			hlgCraft = null;
			btnConfirmCraft = null;
			imgPlayEnd = null;
			btnPlayEnd = null;
			titleGroup = null;
			btnSubmitTitle = null;
			Content = null;
			imgPlay = null;
			btnConfirmPlay = null;
			btnCancelPlay = null;
			
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
