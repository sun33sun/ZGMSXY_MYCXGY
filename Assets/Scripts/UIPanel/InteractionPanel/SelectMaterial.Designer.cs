/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class SelectMaterial
	{
		[SerializeField] public UnityEngine.UI.Button btnConfirmMaterial;

		public void Clear()
		{
			btnConfirmMaterial = null;
		}

		public override string ComponentName
		{
			get { return "SelectMaterial";}
		}
	}
}
