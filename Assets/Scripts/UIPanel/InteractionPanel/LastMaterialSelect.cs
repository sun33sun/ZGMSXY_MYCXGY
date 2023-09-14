/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class LastMaterialSelect : UIElement
	{
		[SerializeField] private List<Toggle> togMaterials;
		
		private void Awake()
		{
			for (int i = 0; i < togMaterials.Count; i++)
			{
				togMaterials[i].AddAwaitAction(async isOn=>
				{
					if (isOn)
					{
						RectTransform rect = UIKit.GetPanel<InteractionPanel>().imgNext;
						rect.gameObject.SetActive(true);
						await rect.DOLocalMoveY(0, 0.5f).AsyncWaitForCompletion();
					}
				});
			}
			
			float maxDistance = ((togMaterials.Count - 5) / 2) * 140;
			btnLeftCountry.AddAwaitAction(async () =>
			{
				float nowX = Content.localPosition.x;
				if (nowX > -maxDistance)
				{
					Content.DOLocalMoveX(nowX - 140, 0.1f);
					await UniTask.Delay(Settings.smallDelay);
				}
			});
			btnRightcountry.AddAwaitAction(async () =>
			{
				float nowX = Content.localPosition.x;
				if (nowX < maxDistance)
				{
					Content.DOLocalMoveX(nowX + 140, 0.1f);
					await UniTask.Delay(Settings.smallDelay);
				}
			});
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}