/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;

namespace ZGMSXY_MYCXGY
{
    public partial class DescriptionGroup : UIElement
    {
        [SerializeField] private List<Toggle> togDescriptions;
        private int centerIndex;
        Vector3 smallScale = new Vector3(0.82f, 0.82f, 1);

        private void Awake()
        {
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            float maxDistance = ((togDescriptions.Count - 5) / 2) * 290;
            centerIndex = togDescriptions.Count / 2;
            btnLeftDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnLeftDescription, async () =>
            {
                float nowX = Content.anchoredPosition.x;
                if (nowX > -maxDistance)
                {
                    Content.DOAnchorPosX(nowX - 290, 0.1f);
                    togDescriptions[centerIndex].isOn = false;
                    togDescriptions[centerIndex].transform.DOScale(smallScale, 0.1f);
                    centerIndex++;
                    togDescriptions[centerIndex].isOn = true;
                    togDescriptions[centerIndex].transform.DOScale(Vector3.one, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            }, token));
            btnRightDescription.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnRightDescription, async () =>
            {
                float nowX = Content.anchoredPosition.x;
                if (nowX < maxDistance)
                {
                    Content.DOAnchorPosX(nowX + 290, 0.1f);
                    togDescriptions[centerIndex].isOn = false;
                    togDescriptions[centerIndex].transform.DOScale(smallScale, 0.1f);
                    centerIndex--;
                    togDescriptions[centerIndex].isOn = true;
                    togDescriptions[centerIndex].transform.DOScale(Vector3.one, 0.1f);
                    await UniTask.Delay(Settings.smallDelay);
                }
            }, token));
        }

        protected override void OnBeforeDestroy()
        {
        }

        private void OnEnable()
        {
            togDescriptions[centerIndex].isOn = false;
            togDescriptions[centerIndex].transform.localScale = smallScale;
            centerIndex = togDescriptions.Count / 2;
            togDescriptions[centerIndex].isOn = true;
            togDescriptions[centerIndex].transform.localScale = Vector3.one;
            Content.anchoredPosition = Vector2.zero;
        }
    }
}