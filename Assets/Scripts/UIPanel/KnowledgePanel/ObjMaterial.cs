/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjMaterial : UIElement
	{
		[SerializeField] List<Button> materialItems;
		int nowMaterialIndex = 0;

		[SerializeField] List<Button> entityItems;
		int nowEntityIndex = 0;

		private void Awake()
		{
			vpAnimationGroup.url = ExtensionFunction.VideoPath;
			
			Func<Transform,UniTask> asLastShoAsync = GetAsLastShowAsync();
			//togMap
			togMap.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					await asLastShoAsync(materialGroup);
				}
				else
					await materialGroup.HideAsync();
			});
			
			//togEntity
			togEntity.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					await asLastShoAsync(entityGroup);
				}
				else
					await entityGroup.HideAsync();
			});
			
			//ToAnimation
			togAnimation.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					await asLastShoAsync(vpAnimationGroup.transform);
				}
				else
					await vpAnimationGroup.HideAsync();
			});

			//materialItems
			for (int i = 0; i < materialItems.Count; i++)
			{
				int index = i;
				materialItems[i].AddAwaitAction(async () =>
				{
					await asLastShoAsync(materialGroup);
					print(index);
				});
			}

			//btnCloseMaterialDescription
			btnCloseMaterialDescription.AddAwaitAction(async () =>
			{
				await materialDescription.HideAsync();
				await asLastShoAsync(materialGroup);
			});

			//entityItems
			for (int i = 0; i < entityItems.Count; i++)
			{
				int index = i;
				entityItems[i].AddAwaitAction(async () =>
				{
					await entityDescription.HideAsync();
					print(index);
				});
			}
			
			btnCloseEntityDescription.AddAwaitAction(async ()=> await entityDescription.HideAsync());

			btnStartAnim.AddAwaitAction(() =>
			{
				if(vpAnimationGroup.isPlaying)
					return;
				vpAnimationGroup.Play();
				animationRawImage.gameObject.SetActive(true);
			});
		}
		
		Func<Transform,UniTask> GetAsLastShowAsync()
		{
			return async t =>
			{
				t.SetAsLastSibling();
				await t.ShowAsync();
			};
		}

		public override void Show()
		{
			base.Show();

			vpAnimationGroup.ShowSync();
			
			materialDescription.HideSync();
			
			entityGroup.HideSync();
			entityDescription.HideSync();

			togAnimation.isOn = true;
			togMap.isOn = false;
			togEntity.isOn = false;
        }

		public override void Hide()
		{
			vpAnimationGroup.Stop();
			animationRawImage.gameObject.SetActive(false);
			base.Hide();
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}