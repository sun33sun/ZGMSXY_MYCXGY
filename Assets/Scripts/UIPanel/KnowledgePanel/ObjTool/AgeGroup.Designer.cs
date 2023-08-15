/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class AgeGroup
	{
		[SerializeField] public RectTransform Content;
		[SerializeField] public UnityEngine.UI.Button btnLeftAge;
		[SerializeField] public UnityEngine.UI.Button btnRightAge;

		public void Clear()
		{
			Content = null;
			btnLeftAge = null;
			btnRightAge = null;
		}

		public override string ComponentName
		{
			get { return "AgeGroup";}
		}
	}
}
