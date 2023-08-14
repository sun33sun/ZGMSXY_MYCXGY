using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace ZGMSXY_MYCXGY
{
	public class InteractionPanelData : UIPanelData
	{
	}
	public partial class InteractionPanel : UIPanel
	{
		List<GameObject> groups = new List<GameObject>();
		CancellationTokenSource cts;

		protected override void OnInit(IUIData uiData = null)
		{
			InitGroups();

			mData = uiData as InteractionPanelData ?? new InteractionPanelData();

			CancellationToken token = this.GetCancellationTokenOnDestroy();

			btnBack.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
			{
				Hide();
				await UniTask.WaitUntil(() => !gameObject.activeInHierarchy);
				UIKit.GetPanel<MainPanel>().Show();
			}, token));

			btnConfirmNext.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmNext, async () =>
			{
				imgNext.transform.DOLocalMoveY(1080, 0.5f);
				materialGroup.transform.DOLocalMoveY(-800, 0.4f);
				await UniTask.Delay(Settings.HideDelay);
				imgNext.gameObject.SetActive(false);
				materialGroup.gameObject.SetActive(false);
				NextState();
				ModelRoot.Instance.gameObject.SetActive(true);
				//等待点击Cube
				UniTask.Void(async t => { 
					await ModelRoot.Instance.WaitClickCube();
					imgPlayRealVideo.gameObject.SetActive(true);
					imgPlayRealVideo.transform.DOLocalMoveY(0, 0.5f);
				}, cts.Token);
			}, token));

			btnCancelNext.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCancelNext, async () =>
			{
				imgNext.transform.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				imgNext.gameObject.SetActive(false);
				imgPlayRealVideo.gameObject.SetActive(true);
				imgPlayRealVideo.DOLocalMoveY(0, 0.5f);
			}, token));

			btnConfirmPlayRealVideo.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmPlayRealVideo, async () =>
			{
				imgPlayRealVideo.transform.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				imgPlayRealVideo.gameObject.SetActive(false);
				NextState();
				vpRealVideo.gameObject.SetActive(true);
				vpRealVideo.Play();
				await UniTask.Yield(PlayerLoopTiming.Update);
				UniTask.Void(async t => {
					await UniTask.WaitUntil(() => !vpRealVideo.isPlaying);
					vpRealVideo.gameObject.SetActive(false);
					imgSubmitModel.gameObject.SetActive(true);
					imgSubmitModel.DOLocalMoveY(0, 0.5f);
				}, cts.Token);
			}, token));

			btnCancelPlayRealVideo.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCancelPlayRealVideo, async () =>
			{
				imgPlayRealVideo.transform.DOLocalMoveY(1080, 0.5f);
				await UniTask.Delay(Settings.HideDelay);
				imgPlayRealVideo.gameObject.SetActive(false);
				NextState();
				imgSubmitModel.gameObject.SetActive(true);
				imgSubmitModel.DOLocalMoveY(0, 0.5f);
			}, token));

			btnConfirmSubmitModel.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnConfirmSubmitModel, async () =>
			{
				imgSubmitModel.DOLocalMoveY(1080, 0.5f);
				imgSubmitModel.gameObject.SetActive(false);
				//进入评测模块
				UIKit.GetPanel<EvaluatePanel>().Show();
			}, token));

			btnCancelSubmitModel.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnCancelSubmitModel, async () =>
			{
				imgSubmitModel.DOLocalMoveY(1080, 0.5f);
				imgSubmitModel.gameObject.SetActive(false);
				await UniTask.Delay(Settings.HideDelay);
				btnEnterEvaluate.gameObject.SetActive(true);
			}, token));

			btnEnterEvaluate.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnEnterEvaluate, async () =>
			{
				imgSubmitModel.DOLocalMoveY(0, 0.5f);
				imgSubmitModel.gameObject.SetActive(true);
				await UniTask.Delay(Settings.HideDelay);
				btnEnterEvaluate.gameObject.SetActive(false);
			}, token));
		}

		void InitGroups() 
		{
			groups.Add(materialGroup.gameObject);

		}

		private void OnEnable()
		{
			cts = new CancellationTokenSource();
		}

		private void OnDisable()
		{
			if(cts != null)
			{
				cts.Cancel();
				cts.Dispose();
			}
		}


		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		public override async void Show()
		{
			base.Show();
			vpRealVideo.gameObject.SetActive(false);
			materialGroup.transform.localPosition = new Vector3(20, -421, 0);
			imgNext.gameObject.SetActive(false);
			imgNext.transform.localPosition = new Vector3(0, 1080, 0);
			imgPlayRealVideo.gameObject.SetActive(false);
			imgPlayRealVideo.transform.localPosition = new Vector3(0, 1080, 0);
			imgSubmitModel.gameObject.SetActive(false);
			imgSubmitModel.transform.localPosition = new Vector3(0, 1080, 0);
			btnEnterEvaluate.gameObject.SetActive(false);
			btnEnterEvaluate.transform.localPosition = new Vector3(0, 1080, 0);
			transform.DOLocalMoveY(0, 0.5f);
		}

		public override async void Hide()
		{
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(Settings.HideDelay);
			base.Hide();
		}

		public void NextState()
		{
			taskSchedule.NextState();
		}
	}
}
