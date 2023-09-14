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
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public partial class ObjTool : UIElement
	{
		private void Awake()
		{
			CancellationToken token = this.GetCancellationTokenOnDestroy();

			togDescription.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					descriptionGroup.transform.SetAsLastSibling();
					await descriptionGroup.ShowAsync();
				}
				else
					await descriptionGroup.HideAsync();
			});

			togPrinciple.AddAwaitAction(async isOn=>
			{
				if (isOn)
				{
					principleGroup.transform.SetAsLastSibling();
					await principleGroup.ShowAsync();
				}
				else
				{
					await principleGroup.HideAsync();
				}
			});

			togAge.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					ageGroup.transform.SetAsLastSibling();
					await ageGroup.ShowAsync();
				}
				else
				{
					await ageGroup.HideAsync();
				}
			});

			togCountry.AddAwaitAction(async isOn =>
			{
				if (isOn)
				{
					togCountry.transform.SetAsLastSibling();
					await countryGroup.ShowAsync();
				}
				else
				{
					await countryGroup.HideAsync();					
				}
			});

			btnPlayEnd.AddAwaitAction(async () =>
			{
				await imgPlayEnd.ShowAsync();
			});
		}

		public override void Show()
		{
			base.Show();
			
			principleGroup.ShowSync();
			
			descriptionGroup.HideSync();

			ageGroup.HideSync();
			
			countryGroup.HideSync();

			imgPlayEnd.gameObject.SetActive(false);
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void Reset()
		{
			togDescription.isOn = true;
		}
	}
}