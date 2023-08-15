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

namespace ZGMSXY_MYCXGY
{
    public partial class MaterialGroup : UIElement
    {
        [SerializeField] List<Toggle> togMaterials;

        private void Awake()
        {
            CancellationToken token = this.GetCancellationTokenOnDestroy();

            for (int i = 0; i < togMaterials.Count; i++)
            {
                Func<bool> funcMaterials = Settings.GetToggleAnimatorEndFunc(togMaterials[i]);
                togMaterials[i].onValueChanged.AddListener(async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                    {
                        UIRoot.Instance.GraphicRaycaster.enabled = false;
                        await UniTask.WaitUntil(funcMaterials);
                        RectTransform rect = UIKit.GetPanel<InteractionPanel>().imgNext;
                        rect.gameObject.SetActive(true);
                        rect.DOLocalMoveY(0, 0.5f);
                        await UniTask.Delay(Settings.HideDelay);
                        UIRoot.Instance.GraphicRaycaster.enabled = true;
                    }
                    else
                    {
                    }
                });
            }

            float maxDistance = ((togMaterials.Count - 5) / 2) * 140;
            btnLeftCountry.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLeftCountry, async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX > -maxDistance)
                {
                    Content.DOLocalMoveX(nowX - 140, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            }, token));
            btnRightcountry.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnRightcountry, async () =>
            {
                float nowX = Content.localPosition.x;
                if (nowX < maxDistance)
                {
                    Content.DOLocalMoveX(nowX + 140, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            }, token));
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