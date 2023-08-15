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

namespace ZGMSXY_MYCXGY
{
	public partial class CountryGroup : UIElement
	{
		[SerializeField] private List<RectTransform> countryItems;
		private int centerIndex;
		private void Awake()
		{
			CancellationToken token = this.GetCancellationTokenOnDestroy();
			float maxDistance = ((countryItems.Count - 1) / 2) * 956;
			centerIndex = countryItems.Count / 2;
			btnLeftCountry.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLeftCountry, async () =>
			{
				float nowX = Content.anchoredPosition.x;
				if (nowX > -maxDistance)
				{
					Content.DOAnchorPosX(nowX - 956, 0.1f);
					centerIndex++;
					await UniTask.Delay(Settings.smallDelay);
				}
			}, token));
			btnRightcountry.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnRightcountry, async () =>
			{
				float nowX = Content.anchoredPosition.x;
				if (nowX < maxDistance)
				{
					Content.DOAnchorPosX(nowX + 956, 0.1f);
					centerIndex--;
					await UniTask.Delay(Settings.smallDelay);
				}
			}, token));
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		protected  void OnEnable()
		{
			Content.anchoredPosition = new Vector2(468, 0);
		}
	}
}