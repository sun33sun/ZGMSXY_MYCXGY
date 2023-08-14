/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjTool : UIElement
	{
		[SerializeField] List<Toggle> principleItems;
		private void Awake()
		{
			CancellationToken token = this.GetCancellationTokenOnDestroy();

			Func<bool> funcDescription = Settings.GetToggleAnimatorEndFunc(togDescription);
			togDescription.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcDescription);
					await UniTask.Delay(Settings.HideDelay);
					descriptionGroup.gameObject.SetActive(true);
					descriptionGroup.transform.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					descriptionGroup.transform.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.HideDelay);
					descriptionGroup.gameObject.SetActive(false);
				}
			});

			Func<bool> funcPrinciple = Settings.GetToggleAnimatorEndFunc(togPrinciple);
			togPrinciple.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcPrinciple);
					await UniTask.Delay(Settings.HideDelay);
					principleGroup.gameObject.SetActive(true);
					principleGroup.transform.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					principleGroup.transform.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.HideDelay);
					principleGroup.gameObject.SetActive(false);
				}

			});

			Func<bool> funcAge = Settings.GetToggleAnimatorEndFunc(togAge);
			togAge.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcPrinciple);
					await UniTask.Delay(Settings.HideDelay);
					ageGroup.gameObject.SetActive(true);
					ageGroup.transform.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					ageGroup.transform.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.HideDelay);
					ageGroup.gameObject.SetActive(false);
				}
			});

			Func<bool> funcCountry = Settings.GetToggleAnimatorEndFunc(togCountry);
			togCountry.onValueChanged.AddListener(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				if (isOn)
				{
					UIRoot.Instance.GraphicRaycaster.enabled = false;
					await UniTask.WaitUntil(funcPrinciple);
					await UniTask.Delay(Settings.HideDelay);
					countryGroup.gameObject.SetActive(true);
					countryGroup.transform.DOLocalMoveY(0, 0.5f);
					await UniTask.Delay(Settings.ShowDelay);
					UIRoot.Instance.GraphicRaycaster.enabled = true;
				}
				else
				{
					countryGroup.transform.DOLocalMoveY(1080, 0.5f);
					await UniTask.Delay(Settings.HideDelay);
					countryGroup.gameObject.SetActive(false);
				}
			});

			btnPlayEnd.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnPlayEnd, async () =>
			 {
				 imgPlayEnd.transform.DOLocalMoveY(1080, 0.5f);
				 await UniTask.Delay(Settings.HideDelay);
				 imgPlayEnd.gameObject.SetActive(false);
			 }, token));

			Vector2 bigSize = new Vector2(240, 240);
			Vector2 smallSize = new Vector2(190, 190);
			for (int i = 0; i < principleItems.Count; i++)
			{
				int index = i;
				Func<bool> principleItemsFunc = Settings.GetToggleAnimatorEndFunc(principleItems[i]);
				RectTransform rect = principleItems[i].transform as RectTransform;
				principleItems[i].onValueChanged.AddListener(async isOn =>
				{
					if (token.IsCancellationRequested) return;
					if (isOn)
					{
						UIRoot.Instance.GraphicRaycaster.enabled = false;
						await UniTask.WaitUntil(principleItemsFunc);
						await UniTask.Delay(Settings.HideDelay);
						rect.DOSizeDelta(bigSize, 0.5f);
						await UniTask.Delay(Settings.ShowDelay);
						UIRoot.Instance.GraphicRaycaster.enabled = true;
						LayoutRebuilder.ForceRebuildLayoutImmediate(hlgPrinciple);
					}
					else
					{
						rect.DOSizeDelta(smallSize, 0.5f);
						await UniTask.Delay(Settings.ShowDelay);
					}
				});
			}
		}

		public override void Show()
		{
			base.Show();
			Vector3 hidePos = new Vector3(0, 1080, 0);

			descriptionGroup.gameObject.SetActive(true);
			descriptionGroup.transform.localPosition = Vector3.zero;

			principleGroup.gameObject.SetActive(false);
			principleGroup.transform.localPosition = hidePos;

			ageGroup.gameObject.SetActive(false);
			ageGroup.transform.localPosition = hidePos;
			countryGroup.gameObject.SetActive(false);
			countryGroup.transform.localPosition = hidePos;

			imgPlayEnd.gameObject.SetActive(false);
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}