﻿/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class LastMaterialSelect
	{
		[SerializeField] public UnityEngine.RectTransform Content;
		[SerializeField] public UnityEngine.UI.Button btnLeftCountry;
		[SerializeField] public UnityEngine.UI.Button btnRightcountry;

		public void Clear()
		{
			Content = null;
			btnLeftCountry = null;
			btnRightcountry = null;
		}

		public override string ComponentName
		{
			get { return "LastMaterialSelect";}
		}
	}
}
