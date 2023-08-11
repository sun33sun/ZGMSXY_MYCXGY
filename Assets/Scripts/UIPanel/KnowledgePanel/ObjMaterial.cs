/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

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
			GameObject topMask = UIKit.GetPanel<TopPanel>().imgMask.gameObject;
			CancellationToken token = this.GetCancellationTokenOnDestroy();
			//togMap
			Func<bool> funcMap = Settings.GetToggleAnimatorEndFunc(togMap);
			togMap.onValueChanged.AddListener(async isOn=> 
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					topMask.SetActive(true);
					await UniTask.WaitUntil(funcMap);
					await UniTask.Delay(500);
					materialGroup.gameObject.SetActive(true);
					materialGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(600);
					topMask.SetActive(false);
				}
				else
				{
					materialGroup.DOLocalMoveY(1080, 0.5f);
					materialDescription.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(600);
					materialGroup.gameObject.SetActive(false);
					materialDescription.gameObject.SetActive(false);
				}
			});
			//togEntity
			Func<bool> funcEntity = Settings.GetToggleAnimatorEndFunc(togEntity);
			togEntity.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					topMask.SetActive(true);
					await UniTask.WaitUntil(funcEntity);
					await UniTask.Delay(500);
					entityGroup.gameObject.SetActive(true);
					entityGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(600);
					topMask.SetActive(false);
				}
				else
				{
					entityGroup.DOLocalMoveY(1080, 0.5f);
					entityDescription.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(600);
					entityGroup.gameObject.SetActive(false);
					entityDescription.gameObject.SetActive(false);
				}
			});
			//ToAnimation
			Func<bool> funcAnimation = Settings.GetToggleAnimatorEndFunc(togAnimation);
			togAnimation.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					topMask.SetActive(true);
					await UniTask.WaitUntil(funcAnimation);
					await UniTask.Delay(500);
					animationGroup.gameObject.SetActive(true);
					animationGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(600);
					topMask.SetActive(false);
				}
				else
				{
					animationGroup.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(600);
					animationGroup.gameObject.SetActive(false);
				}
			});

			//materialItems
			for (int i = 0; i < materialItems.Count; i++)
			{
				int index = i;
				materialItems[i].onClick.AddListener(Settings.GetButtonIgnoreClickFunc(materialItems[i],async ()=> 
				{
					materialDescription.gameObject.SetActive(true);
					materialDescription.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(600);
					print(index);
				},token));
			}

			//btnCloseMaterialDescription
			btnCloseMaterialDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCloseMaterialDescription,async()=>
			{
				materialDescription.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(600);
				materialDescription.gameObject.SetActive(false);
			}, token));

			//entityItems
			for (int i = 0; i < entityItems.Count; i++)
			{
				int index = i;
				entityItems[i].onClick.AddListener(Settings.GetButtonIgnoreClickFunc(entityItems[i],async ()=> 
				{
					entityDescription.gameObject.SetActive(true);
					entityDescription.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(600);
					print(index);
				},token));
			}
			btnCloseEntityDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCloseEntityDescription, async () =>
			 {
				 entityDescription.DOLocalMoveY(0, 0.5f);
				 await UniTask.Delay(600);
				 entityDescription.gameObject.SetActive(false);
			 }, token));
		}

		public override void Show()
		{
			base.Show();
			Vector3 hidePos = new Vector3(0, 1080, 0);

			materialGroup.gameObject.SetActive(true);
			materialGroup.transform.localPosition = Vector3.zero;

			materialDescription.gameObject.SetActive(false);
			materialDescription.transform.localPosition = hidePos;

			entityGroup.gameObject.SetActive(false);
			entityGroup.transform.localPosition = hidePos;
			entityDescription.gameObject.SetActive(false);
			entityDescription.transform.localPosition = hidePos;
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}