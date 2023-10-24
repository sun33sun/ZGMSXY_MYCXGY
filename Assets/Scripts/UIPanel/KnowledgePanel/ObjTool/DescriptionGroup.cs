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
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public partial class DescriptionGroup : UIElement
    {
        [SerializeField] HorizontalSegmentation hsSelf;
        [SerializeField] private List<Toggle> togDescriptions;
        private int centerIndex;
        Vector3 smallScale = new Vector3(0.82f, 0.82f, 1);

        private void Awake()
        {

            btnLeftDescription.onClick.AddListener(() =>
            {
                togDescriptions[centerIndex].transform.DOScale(smallScale,hsSelf.duration);
                centerIndex++;
                togDescriptions[centerIndex].transform.DOScale(Vector3.one,hsSelf.duration);
            });

            btnRightDescription.onClick.AddListener(() =>
            {
				togDescriptions[centerIndex].transform.DOScale(smallScale, hsSelf.duration);
				centerIndex--;
				togDescriptions[centerIndex].transform.DOScale(Vector3.one, hsSelf.duration);
			});
        }

        protected override void OnBeforeDestroy()
        {
        }

        private void OnEnable()
        {
			ResetState();
        }

        void ResetState()
        {
            togDescriptions[centerIndex].transform.localScale = smallScale;
            centerIndex = hsSelf.Content.childCount / 2;
            togDescriptions[centerIndex].transform.localScale = Vector3.one;

			hsSelf.ResetState();
		}

	}
}