using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	// Generate Id:09636454-64d1-4731-9481-b4dabd4f775c
	public partial class MainPanel
	{
		public const string Name = "MainPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpModuleName;
		[SerializeField]
		public RectTransform objStart;
		[SerializeField]
		public UnityEngine.UI.Button btnStart;
		[SerializeField]
		public RectTransform ButtonGroup;
		[SerializeField]
		public UnityEngine.UI.Button btnPreview;
		[SerializeField]
		public UnityEngine.UI.Button btnKnowledge;
		[SerializeField]
		public UnityEngine.UI.Button btnLearn;
		[SerializeField]
		public UnityEngine.UI.Button btninteraction;
		[SerializeField]
		public UnityEngine.UI.Button btnReport;
		[SerializeField]
		public RectTransform SecondButtonGroup;
		[SerializeField]
		public UnityEngine.UI.Button btnTool;
		[SerializeField]
		public UnityEngine.UI.Button btnMaterial;
		[SerializeField]
		public UnityEngine.UI.Button btnCase;
		
		private MainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			tmpModuleName = null;
			objStart = null;
			btnStart = null;
			ButtonGroup = null;
			btnPreview = null;
			btnKnowledge = null;
			btnLearn = null;
			btninteraction = null;
			btnReport = null;
			SecondButtonGroup = null;
			btnTool = null;
			btnMaterial = null;
			btnCase = null;
			
			mData = null;
		}
		
		public MainPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		MainPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MainPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
