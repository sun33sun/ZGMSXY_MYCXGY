/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class PrincipleGroup
	{
		[SerializeField] public UnityEngine.RectTransform hlgPrinciple;

		public void Clear()
		{
			hlgPrinciple = null;
		}

		public override string ComponentName
		{
			get { return "PrincipleGroup";}
		}
	}
}
