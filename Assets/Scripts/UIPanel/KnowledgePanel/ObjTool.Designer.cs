/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjTool
	{
		[SerializeField] public UnityEngine.UI.Toggle togDescription;
		[SerializeField] public UnityEngine.UI.Toggle togPrinciple;
		[SerializeField] public UnityEngine.UI.Toggle togAge;
		[SerializeField] public UnityEngine.UI.Toggle togCountry;
		[SerializeField] public UnityEngine.UI.ScrollRect descriptionGroup;
		[SerializeField] public RectTransform principleGroup;
		[SerializeField] public UnityEngine.RectTransform hlgPrinciple;
		[SerializeField] public RectTransform ageGroup;
		[SerializeField] public UnityEngine.UI.Button btnLeftAge;
		[SerializeField] public UnityEngine.UI.Button btnRightAge;
		[SerializeField] public RectTransform countryGroup;
		[SerializeField] public UnityEngine.UI.Button btnLeftCountry;
		[SerializeField] public UnityEngine.UI.Button btnRightcountry;
		[SerializeField] public UnityEngine.UI.Image imgPlayEnd;
		[SerializeField] public UnityEngine.UI.Button btnPlayEnd;

		public void Clear()
		{
			togDescription = null;
			togPrinciple = null;
			togAge = null;
			togCountry = null;
			descriptionGroup = null;
			principleGroup = null;
			hlgPrinciple = null;
			ageGroup = null;
			btnLeftAge = null;
			btnRightAge = null;
			countryGroup = null;
			btnLeftCountry = null;
			btnRightcountry = null;
			imgPlayEnd = null;
			btnPlayEnd = null;
		}

		public override string ComponentName
		{
			get { return "ObjTool";}
		}
	}
}
