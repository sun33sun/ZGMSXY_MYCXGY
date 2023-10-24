/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class ToolSelections
	{
		[SerializeField] public UnityEngine.UI.Image SelectedTool;
		[SerializeField] public UnityEngine.RectTransform SeletedToolParent;
		[SerializeField] public TMPro.TextMeshProUGUI tmpRightTip;
		[SerializeField] public UnityEngine.UI.Button btnConfirmTool;
		[SerializeField] public ProjectBase.HorizontalSegmentation hsTool;
		[SerializeField] public UnityEngine.UI.Button btnLeftTool;
		[SerializeField] public UnityEngine.UI.Button btnRightTool;

		public void Clear()
		{
			SelectedTool = null;
			SeletedToolParent = null;
			tmpRightTip = null;
			btnConfirmTool = null;
			hsTool = null;
			btnLeftTool = null;
			btnRightTool = null;
		}

		public override string ComponentName
		{
			get { return "ToolSelections";}
		}
	}
}
