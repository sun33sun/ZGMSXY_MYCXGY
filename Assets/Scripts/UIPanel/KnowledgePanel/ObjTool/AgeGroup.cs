/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using QFramework;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public partial class AgeGroup : UIElement
	{
		[SerializeField] HorizontalSegmentation hsSelf;
		[SerializeField] private List<RectTransform> ageItems;
		int centerIndex;
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		private void OnEnable()
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