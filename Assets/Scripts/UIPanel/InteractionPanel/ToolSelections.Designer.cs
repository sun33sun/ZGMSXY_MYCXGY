/****************************************************************************
 * 2023.9 ADMIN-20230222V
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
		[SerializeField] public UnityEngine.UI.Button btnConfirmTool;
		[SerializeField] public UnityEngine.RectTransform Content;
		[SerializeField] public UnityEngine.UI.Button btnLeftTool;
		[SerializeField] public UnityEngine.UI.Button btnRightTool;

		public void Clear()
		{
			SelectedTool = null;
			SeletedToolParent = null;
			btnConfirmTool = null;
			Content = null;
			btnLeftTool = null;
			btnRightTool = null;
		}

		public override string ComponentName
		{
			get { return "ToolSelections";}
		}
	}
}
