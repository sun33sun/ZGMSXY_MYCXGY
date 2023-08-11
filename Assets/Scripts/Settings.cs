using Cysharp.Threading.Tasks;
using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZGMSXY_MYCXGY
{
	public static class Settings
	{
		public static string UI { get { return "Resources/UIPanel/"; } }

		public static Func<bool> GetButtonAnimatorEndFunc(Button btn)
		{
			Animator animator = btn.animator;
			return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
		}

		public static UnityAction GetButtonIgnoreClickFunc(Button btn, Func<UniTask> invoke, CancellationToken token)
		{
			Func<bool> func = GetButtonAnimatorEndFunc(btn);
			GameObject topMask = UIKit.GetPanel<TopPanel>().imgMask.gameObject;
			return async () =>
			{
				if (token.IsCancellationRequested) return;
				await UniTask.WaitUntil(func);
				topMask.gameObject.SetActive(true);
				await invoke();
				topMask.gameObject.SetActive(false);
			};
		}

		public static Func<bool> GetToggleAnimatorEndFunc(Toggle tog)
		{
			Animator animator = tog.animator;
			return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
		}
	}
}
