/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class PrincipleGroup : UIElement
	{
		[SerializeField] List<Toggle> principleItems;
		Vector2 bigSize = new Vector2(240, 240);
		Vector2 smallSize = new Vector2(190, 190);
		
		private void Awake()
		{

			for (int i = 0; i < principleItems.Count; i++)
			{
				int index = i;
				Func<bool> principleItemsFunc = Settings.GetToggleAnimatorEndFunc(principleItems[i]);
				RectTransform rect = principleItems[i].transform as RectTransform;
				principleItems[i].onValueChanged.AddListener(async isOn =>
				{
					if (isOn)
					{
						rect.sizeDelta = bigSize;
						await UniTask.Yield(PlayerLoopTiming.Update);
						LayoutRebuilder.ForceRebuildLayoutImmediate(hlgPrinciple);
					}
					else
					{
						rect.sizeDelta = smallSize;
					}
				});
			}
		}

		protected override void OnBeforeDestroy()
		{
		}

		private void OnEnable()
		{
			for (int i = 0; i < principleItems.Count(); i++)
			{
				if (principleItems[i].isOn)
					(principleItems[i].transform as RectTransform).sizeDelta = smallSize;
			}
			principleItems[0].isOn = true;
			(principleItems[0].transform as RectTransform).sizeDelta = bigSize;
		}
	}
}