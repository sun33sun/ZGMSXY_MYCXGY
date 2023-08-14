using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;

namespace ZGMSXY_MYCXGY
{
	public class ModelRoot : SingletonMono<ModelRoot>
	{
		public Camera modelC;
		public Light dirLight;
		public EventTrigger cube;
		CancellationTokenSource cts = null;
		public Image imgTip;

		protected override void Awake()
		{
			base.Awake();
			UIEventTool.AddEventTrigger(cube.gameObject, EventTriggerType.PointerEnter, _ => imgTip.gameObject.SetActive(true));
			UIEventTool.AddEventTrigger(cube.gameObject, EventTriggerType.PointerExit, _ => imgTip.gameObject.SetActive(false));
			gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			cts = new CancellationTokenSource();
		}

		private void OnDisable()
		{
			if (cts != null)
			{
				cts.Cancel();
				cts.Dispose();
			}
		}

		public async UniTask WaitClickCube()
		{
			cube.gameObject.SetActive(true);
			await cube.triggers.Find(t => t.eventID == EventTriggerType.PointerClick).callback.OnInvokeAsync(cts.Token);
			cube.gameObject.SetActive(false);
			imgTip.gameObject.SetActive(false);
		}
	}
}

