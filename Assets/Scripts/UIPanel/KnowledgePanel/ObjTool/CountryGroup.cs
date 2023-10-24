/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public partial class CountryGroup : UIElement
	{
		[SerializeField] HorizontalSegmentation hsSelf;
		[SerializeField] private List<RectTransform> countryItems;
		private int centerIndex;
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		protected  void OnEnable()
		{
			ResetState();
		}

		void ResetState()
		{
			centerIndex = hsSelf.Content.childCount / 2;
			hsSelf.ResetState();
		}
	}
}