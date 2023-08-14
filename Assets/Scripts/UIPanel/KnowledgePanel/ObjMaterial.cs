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
			CancellationToken token = this.GetCancellationTokenOnDestroy();
			//togMap
			Func<bool> funcMap = Settings.GetToggleAnimatorEndFunc(togMap);
			togMap.onValueChanged.AddListener(async isOn=> 
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcMap);
					await UniTask.Delay(Settings.HideDelay);
					materialGroup.gameObject.SetActive(true);
					materialGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					materialGroup.DOLocalMoveY(1080, 0.5f);
					materialDescription.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
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
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcEntity);
					await UniTask.Delay(Settings.HideDelay);
					entityGroup.gameObject.SetActive(true);
					entityGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					entityGroup.DOLocalMoveY(1080, 0.5f);
					entityDescription.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
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
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcAnimation);
					await UniTask.Delay(Settings.HideDelay);
					animationGroup.gameObject.SetActive(true);
					animationGroup.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					animationGroup.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.HideDelay);
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
					await UniTask.Delay(Settings.ShowDelay);
					print(index);
				},token));
			}

			//btnCloseMaterialDescription
			btnCloseMaterialDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCloseMaterialDescription,async()=>
			{
				materialDescription.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
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
					await UniTask.Delay(Settings.HideDelay);
					print(index);
				},token));
			}
			btnCloseEntityDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCloseEntityDescription, async () =>
			 {
				 entityDescription.DOLocalMoveY(0, 0.5f);
				 await UniTask.Delay(Settings.HideDelay);
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