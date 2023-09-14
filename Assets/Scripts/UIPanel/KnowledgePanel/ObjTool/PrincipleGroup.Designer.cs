/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class PrincipleGroup
	{
		[SerializeField] public UnityEngine.RectTransform hlgPrinciple;
		[SerializeField] public UnityEngine.UI.Button btnToolDetail;

		public void Clear()
		{
			hlgPrinciple = null;
			btnToolDetail = null;
		}

		public override string ComponentName
		{
			get { return "PrincipleGroup";}
		}
	}
}
