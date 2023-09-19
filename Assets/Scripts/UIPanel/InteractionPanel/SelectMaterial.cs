/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.Serialization;

namespace ZGMSXY_MYCXGY
{
    public partial class SelectMaterial : UIElement
    {
        public enum MaterialType
        {
            m0,
            m1,
            m2
        }

        public MaterialType nowMaterialType => (MaterialType)selectedIndex;
        
        [SerializeField] private List<Toggle> _toggles;
        private int selectedIndex = 0;

        private void Awake()
        {
            for (int i = 0; i < _toggles.Count; i++)
            {
                int index = i;
                _toggles[i].AddAwaitAction(isOn =>
                {
                    if (isOn)
                        selectedIndex = index;
                });
            }

            btnConfirmMaterial.AddAwaitAction(async () =>
            {
                await transform.HideAsync();
            });
        }

        protected override void OnBeforeDestroy()
        {
        }

        public override void Hide()
        {
            selectedIndex = 0;
            _toggles.ForEach(t => t.isOn = false);
            transform.position = Vector3.zero;
            base.Hide();
        }
    }
}