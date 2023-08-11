/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjMaterial
	{
		[SerializeField] public UnityEngine.UI.Toggle togMap;
		[SerializeField] public UnityEngine.UI.Toggle togEntity;
		[SerializeField] public UnityEngine.UI.Toggle togAnimation;
		[SerializeField] public UnityEngine.RectTransform materialGroup;
		[SerializeField] public UnityEngine.RectTransform entityGroup;
		[SerializeField] public UnityEngine.RectTransform materialDescription;
		[SerializeField] public UnityEngine.UI.Image imgMaterialBig;
		[SerializeField] public UnityEngine.UI.Button btnCloseMaterialDescription;
		[SerializeField] public TMPro.TextMeshProUGUI tmpMaterialDescription;
		[SerializeField] public UnityEngine.RectTransform entityDescription;
		[SerializeField] public UnityEngine.UI.Button btnCloseEntityDescription;
		[SerializeField] public TMPro.TextMeshProUGUI tmpEntityDescriptionName;
		[SerializeField] public TMPro.TextMeshProUGUI tmpEntityDescription;
		[SerializeField] public RectTransform animationGroup;

		public void Clear()
		{
			togMap = null;
			togEntity = null;
			togAnimation = null;
			materialGroup = null;
			entityGroup = null;
			materialDescription = null;
			imgMaterialBig = null;
			btnCloseMaterialDescription = null;
			tmpMaterialDescription = null;
			entityDescription = null;
			btnCloseEntityDescription = null;
			tmpEntityDescriptionName = null;
			tmpEntityDescription = null;
			animationGroup = null;
		}

		public override string ComponentName
		{
			get { return "ObjMaterial";}
		}
	}
}
