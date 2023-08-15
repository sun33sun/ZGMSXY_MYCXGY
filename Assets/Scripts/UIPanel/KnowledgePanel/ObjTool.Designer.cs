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
		[SerializeField] public DescriptionGroup descriptionGroup;
		[SerializeField] public PrincipleGroup principleGroup;
		[SerializeField] public AgeGroup ageGroup;
		[SerializeField] public CountryGroup countryGroup;
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
			ageGroup = null;
			countryGroup = null;
			imgPlayEnd = null;
			btnPlayEnd = null;
		}

		public override string ComponentName
		{
			get { return "ObjTool";}
		}
	}
}
