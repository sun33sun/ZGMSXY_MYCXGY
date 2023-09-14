using Cysharp.Threading.Tasks;
using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
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

			return async () =>
			{
				if (token.IsCancellationRequested) return;
				UIRoot.Instance.GraphicRaycaster.enabled = false;
				await UniTask.WaitUntil(func,cancellationToken:token);
				await invoke();
				UIRoot.Instance.GraphicRaycaster.enabled = true;
			};
		}

		public static Func<bool> GetToggleAnimatorEndFunc(Toggle tog)
		{
			Animator animator = tog.animator;
			return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
		}
		


		public const int ShowDelay = 550;
		public const int HideDelay = 500;
		public const int smallDelay = 100;
	}
}
