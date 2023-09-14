/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjMaterial
	{
		[SerializeField] public UnityEngine.UI.Toggle togAnimation;
		[SerializeField] public UnityEngine.UI.Toggle togMap;
		[SerializeField] public UnityEngine.UI.Toggle togEntity;
		[SerializeField] public UnityEngine.RectTransform materialGroup;
		[SerializeField] public UnityEngine.Video.VideoPlayer vpAnimationGroup;
		[SerializeField] public UnityEngine.UI.RawImage animationRawImage;
		[SerializeField] public UnityEngine.UI.Button btnStartAnim;
		[SerializeField] public UnityEngine.RectTransform entityGroup;
		[SerializeField] public UnityEngine.RectTransform materialDescription;
		[SerializeField] public UnityEngine.UI.Image imgMaterialBig;
		[SerializeField] public UnityEngine.UI.Button btnCloseMaterialDescription;
		[SerializeField] public TMPro.TextMeshProUGUI tmpMaterialDescription;
		[SerializeField] public UnityEngine.RectTransform entityDescription;
		[SerializeField] public UnityEngine.UI.Button btnCloseEntityDescription;
		[SerializeField] public TMPro.TextMeshProUGUI tmpEntityDescriptionName;
		[SerializeField] public TMPro.TextMeshProUGUI tmpEntityDescription;

		public void Clear()
		{
			togAnimation = null;
			togMap = null;
			togEntity = null;
			materialGroup = null;
			vpAnimationGroup = null;
			animationRawImage = null;
			btnStartAnim = null;
			entityGroup = null;
			materialDescription = null;
			imgMaterialBig = null;
			btnCloseMaterialDescription = null;
			tmpMaterialDescription = null;
			entityDescription = null;
			btnCloseEntityDescription = null;
			tmpEntityDescriptionName = null;
			tmpEntityDescription = null;
		}

		public override string ComponentName
		{
			get { return "ObjMaterial";}
		}
	}
}
