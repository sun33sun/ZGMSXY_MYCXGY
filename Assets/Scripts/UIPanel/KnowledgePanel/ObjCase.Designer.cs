/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjCase
	{
		[SerializeField] public UnityEngine.UI.ScrollRect imgRightBk;
		[SerializeField] public TMPro.TextMeshProUGUI tmpContent;
		[SerializeField] public RectTransform vlgImage;

		public void Clear()
		{
			imgRightBk = null;
			tmpContent = null;
			vlgImage = null;
		}

		public override string ComponentName
		{
			get { return "ObjCase";}
		}
	}
}
