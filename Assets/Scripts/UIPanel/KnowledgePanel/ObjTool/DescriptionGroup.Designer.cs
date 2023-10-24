/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class DescriptionGroup
	{
		[SerializeField] public UnityEngine.UI.Button btnRightDescription;
		[SerializeField] public UnityEngine.UI.Button btnLeftDescription;

		public void Clear()
		{
			btnRightDescription = null;
			btnLeftDescription = null;
		}

		public override string ComponentName
		{
			get { return "DescriptionGroup";}
		}
	}
}
