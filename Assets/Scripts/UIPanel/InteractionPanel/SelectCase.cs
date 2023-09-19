/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class SelectCase : UIElement
	{
		public enum CaseType
		{
			c0,c1
		}

		[SerializeField] private List<Toggle> _toggles;
		private int selectedIndex = 0;
		public CaseType nowCaseType => (CaseType)selectedIndex;
		

		private void Awake()
		{
			for (int i = 0; i < _toggles.Count; i++)
			{
				int index = i;
				_toggles[i].AddAwaitAction(isOn =>
				{
					if(isOn)
						selectedIndex = index;
				});
			}
			btnConfirmMaterial.AddAwaitAction(async () =>
			{
				await this.HideAsync();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}

		protected override void OnShow()
		{
			selectedIndex = 0;
			_toggles.ForEach(t=>t.isOn =false);
			transform.position = Vector3.zero;
			base.OnShow();
		}
	}
}