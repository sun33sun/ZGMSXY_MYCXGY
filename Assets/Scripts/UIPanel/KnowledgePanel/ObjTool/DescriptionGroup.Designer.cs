/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class DescriptionGroup
	{
		[SerializeField] public RectTransform Content;
		[SerializeField] public UnityEngine.UI.Button btnRightDescription;
		[SerializeField] public UnityEngine.UI.Button btnLeftDescription;

		public void Clear()
		{
			Content = null;
			btnRightDescription = null;
			btnLeftDescription = null;
		}

		public override string ComponentName
		{
			get { return "DescriptionGroup";}
		}
	}
}
