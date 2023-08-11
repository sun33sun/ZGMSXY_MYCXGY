using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Cysharp.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using System.Collections.Generic;

namespace ZGMSXY_MYCXGY
{
	public class KnowledgePanelData : UIPanelData
	{
	}
	public partial class KnowledgePanel : UIPanel
	{
		[SerializeField] Transform MaterialGroup;
		[SerializeField] Transform EntityGroup;
		////工具
		//[SerializeField] List<Toggle> togPrinciples;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgePanelData ?? new KnowledgePanelData();
			MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
			Func<bool> funcBack = () => btnBack.animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
			btnBack.onClick.AddListener(async () =>
			{
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.WaitUntil(funcBack);
				Hide();
				await UniTask.WaitUntil(() => !gameObject.activeInHierarchy);
				UIKit.GetPanel<MainPanel>().Show();
			});
			//CheckButtonClick(btnMaterialItem, async () =>
			//{
			//	imgMaterialDescription.gameObject.SetActive(true);
			//	imgMaterialDescription.transform.DOLocalMoveY(0, 0.5f);
			//	await UniTask.Delay(600);
			//}).Forget();
			//CheckButtonClick(btnCloseMaterialDescription, async () =>
			//{
			//	imgMaterialDescription.transform.DOLocalMoveY(1080, 0.5f);
			//	await UniTask.Delay(600);
			//	imgMaterialDescription.gameObject.SetActive(false);
			//}).Forget();
			//CheckButtonClick(btnEntityItem, async () =>
			//{
			//	EntityDescription.gameObject.SetActive(true);
			//	EntityDescription.transform.DOLocalMoveY(0, 0.5f);
			//	await UniTask.Delay(600);
			//	ModelRoot.Instance.gameObject.SetActive(true);
			//}).Forget();
			//CheckButtonClick(btnCloseEntityDescription, async () =>
			//{
			//	ModelRoot.Instance.gameObject.SetActive(false);
			//	EntityDescription.transform.DOLocalMoveY(1080, 0.5f);
			//	await UniTask.Delay(600);
			//	EntityDescription.gameObject.SetActive(false);
			//}).Forget();
			//CheckToggleClick(togMap, async isOn =>
			// {
			//	 if (isOn)
			//	 {
			//		 await UniTask.Delay(500);
			//		 MaterialGroup.gameObject.SetActive(true);
			//		 MaterialGroup.DOLocalMoveY(0, 0.5f);
			//		 await UniTask.Delay(500);
			//	 }
			//	 else
			//	 {
			//		 MaterialGroup.DOLocalMoveY(1080, 0.5f);
			//		 imgMaterialDescription.transform.DOLocalMoveY(1080, 0.5f);
			//		 await UniTask.Delay(600);
			//		 MaterialGroup.gameObject.SetActive(false);
			//		 imgMaterialDescription.gameObject.SetActive(false);
			//	 }
			// }).Forget();
			//CheckToggleClick(togEntity, async isOn =>
			//{
			//	if (isOn)
			//	{
			//		await UniTask.Delay(500);
			//		EntityGroup.gameObject.SetActive(true);
			//		EntityGroup.DOLocalMoveY(0, 0.5f);
			//		await UniTask.Delay(500);
			//	}
			//	else
			//	{
			//		EntityDescription.transform.DOLocalMoveY(1080, 0.5f);
			//		EntityGroup.DOLocalMoveY(1080, 0.5f);
			//		await UniTask.Delay(600);
			//		EntityGroup.gameObject.SetActive(false);
			//		EntityDescription.gameObject.SetActive(false);
			//	}
			//}).Forget();
			//togDescription.onValueChanged.AddListener(Description);
			//togPrinciple.onValueChanged.AddListener(Principle);

			//for (int i = 0; i < togPrinciples.Count; i++)
			//{
			//	int index = i;
			//	RectTransform rect = togPrinciples[index].transform as RectTransform;
			//	togPrinciples[i].onValueChanged.AddListener(async isOn =>
			//	{
			//		if (isOn)
			//		{
			//			UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
			//			rect.DOSizeDelta(new Vector2(240, 240), 0.1f);
			//			await UniTask.Delay(150);
			//			rect.sizeDelta = new Vector2(240, 240);
			//			UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			//		}
			//		else
			//		{
			//			rect.DOSizeDelta(new Vector2(190, 190), 0.1f);
			//			await UniTask.Delay(150);
			//			rect.sizeDelta = new Vector2(190, 190);
			//		}
			//	});
			//}
		}

		async void Description(bool isOn)
		{
			if (isOn)
			{
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.Delay(500);
				//Description.gameObject.SetActive(true);
				//svDescription.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(600);
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			}
			else
			{
				//svDescription.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(600);
				//svDescription.gameObject.SetActive(false);
			}
		}

		async void Principle(bool isOn)
		{
			if (isOn)
			{
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.Delay(500);
				//PrincipleGroup.gameObject.SetActive(true);
				//togPrinciples[0].isOn = true;
				//PrincipleGroup.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(600);
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			}
			else
			{
				//svDescription.DOLocalMoveY(0, 0.5f);
				await UniTask.Delay(600);
				//svDescription.gameObject.SetActive(false);
			}
		}

		async UniTaskVoid CheckButtonClick(Button btn, Func<UniTask> invoke)
		{
			var asyncEnumerable = btn.OnClickAsAsyncEnumerable();
			var token = this.GetCancellationTokenOnDestroy();
			Animator animator = btn.animator;
			Func<bool> func = () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
			await asyncEnumerable.ForEachAwaitAsync(async _ =>
			{
				if (token.IsCancellationRequested) return;
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.WaitUntil(func);
				await invoke();
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			}, token);
		}

		async UniTaskVoid CheckToggleClick(Toggle tog, Func<bool, UniTask> invoke)
		{
			var asyncEnumerable = tog.OnValueChangedAsAsyncEnumerable();
			var token = this.GetCancellationTokenOnDestroy();
			Animator animator = tog.animator;
			Func<bool> func = () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
			await asyncEnumerable.ForEachAwaitAsync<bool>(async isOn =>
			{
				if (token.IsCancellationRequested) return;
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
				await UniTask.WaitUntil(func);
				await invoke(isOn);
				UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
			}, token);
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			Vector3 hidePos = new Vector3(0, 1080, 0);
			//材料
			MaterialGroup.localPosition = Vector3.zero;
			MaterialGroup.gameObject.SetActive(true);
			EntityGroup.localPosition = hidePos;
			EntityGroup.gameObject.SetActive(false);
			//imgMaterialDescription.transform.localPosition = hidePos;
			//EntityDescription.localPosition = hidePos;
			//EntityDescription.gameObject.SetActive(false);
			//工具
			//svDescription.localPosition = hidePos;
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
			transform.DOLocalMoveY(0, 0.5f);
			await UniTask.Delay(600);
		}

		public override async void Hide()
		{
			UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
			ModelRoot.Instance.gameObject.SetActive(false);
			await UniTask.Delay(500);
			transform.DOLocalMoveY(1080, 0.5f);
			await UniTask.Delay(600);
			objMaterial.Hide();
			objTool.Hide();
			base.Hide();
			UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
		}
	}
}
