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

namespace ZGMSXY_MYCXGY
{
	public partial class AgeGroup : UIElement
	{
		[SerializeField] private List<RectTransform> ageItems;
		int centerIndex;
		private void Awake()
		{
			CancellationToken token = this.GetCancellationTokenOnDestroy();
			float maxDistance = ((ageItems.Count - 1) / 2) * 620;
			centerIndex = ageItems.Count / 2;
			btnLeftAge.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLeftAge, async () =>
			{
				float nowX = Content.anchoredPosition.x;
				if (nowX > -maxDistance)
				{
					Content.DOAnchorPosX(nowX - 620, 0.1f);
					centerIndex++;
					await UniTask.Delay(Settings.smallDelay);
				}
			}, token));
			btnRightAge.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnRightAge, async () =>
			{
				float nowX = Content.anchoredPosition.x;
				if (nowX < maxDistance)
				{
					Content.DOAnchorPosX(nowX + 620, 0.1f);
					centerIndex--;
					await UniTask.Delay(Settings.smallDelay);
				}
			}, token));
		}

		protected override void OnBeforeDestroy()
		{
		}

		private void OnEnable()
		{
			Content.anchoredPosition = new Vector2(310, 0);
		}
	}
}