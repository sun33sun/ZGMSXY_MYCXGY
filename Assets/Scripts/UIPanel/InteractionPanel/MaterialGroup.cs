using System.Net.Mime;
/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public partial class MaterialGroup : UIElement
    {
        [SerializeField] List<Toggle> togMaterials;

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

        private void OnEnable()
        {
            for (int i = 0; i < togMaterials.Count; i++)
            {
                togMaterials[i].isOn = false;
            }
        }
    }
}