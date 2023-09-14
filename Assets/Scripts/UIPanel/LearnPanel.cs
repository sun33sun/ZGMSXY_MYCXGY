using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;
using System.ComponentModel;
using System.Linq;
using ProjectBase;
using UnityEngine.Serialization;

namespace ZGMSXY_MYCXGY
{
	[Serializable]
	public class CraftRes
	{
		public enum CraftType
		{
			[Description("锯切")]
			Sawing,
			[Description("带切")]
			Bandcut,
			[Description("车削")]
			Turnery,
			[Description("钻削")]
			Drilling,
			[Description("铣削")]			
			Milling,
			[Description("磨削")]
			Grinding,
			[Description("凿削")]
			Chiseling,
			[Description("刨削")]
			Slicing,
		}

		public CraftType craftType;
		public string description;
		public Toggle tog;
	}
	
	public class LearnPanelData : UIPanelData
	{
	}
	public partial class LearnPanel : UIPanel
	{
		Vector2 bigSize = new Vector2(240, 240);
		Vector2 smallSize = new Vector2(190, 190);
		[Header("开关对应的资源")]
		[SerializeField] private List<CraftRes> craftRess;
		
		private int selectedIndex = 0;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as LearnPanelData ?? new LearnPanelData();

			//选择工艺环节页面
			btnBack.AddAwaitAction(async () =>
			{
				await this.HideAsyncPanel();
				await UIKit.GetPanel<MainPanel>().ShowAsyncPanel();
			});
			for (int i = 0; i < craftRess.Count; i++)
			{
				int index = i;
				RectTransform rect = craftRess[i].tog.transform as RectTransform;
				craftRess[i].tog.AddAwaitAction(isOn =>
				{
					if (isOn)
					{
						selectedIndex = index;
						rect.sizeDelta = bigSize;
						tmpCraftName.text = "木材的" + craftRess[index].craftType.GetDescription();
						tmpCraftDescription.text = craftRess[index].description;
					}
					else
					{
						rect.sizeDelta = smallSize;
					}
					LayoutRebuilder.ForceRebuildLayoutImmediate(hlgCraft);
				});
			}
			btnConfirmCraft.AddAwaitAction(async () =>
			{
				await imgAnimation.ShowAsync();
				GameManager.Instance.StartLearnAnimation(craftRess[selectedIndex].craftType);
			});
			//工艺动画页面
			btnConfirmAnimation.AddAwaitAction(async () =>await imgAnimationEnd.ShowAsync());
			btnCancelAnimation.AddAwaitAction(async () =>
			{
				GameManager.Instance.EndLearnAnimation(craftRess[selectedIndex].craftType);
				imgAnimation.HideSync();
				await imgAnimationEnd.HideAsync();
			});
			
			btnEnterExamination.AddAwaitAction(async () =>
			{
				await objCraft.HideAsync();
				imgAnimation.HideSync();
				imgAnimationEnd.HideSync();
				await titleGroup.ShowAsync();
			});
			//题目页面
			btnConfirmTitle.AddAwaitAction(async () =>
			{
				await imgDoubleConfirmTitle.ShowAsync();
			});
			btnDoubleConfirmTitle.AddAwaitAction(async () =>
			{
				await imgDoubleConfirmTitle.HideAsync();
				btnSubmitTitle.gameObject.SetActive(true);
			});
			btnDoubleCancelTitle.AddAwaitAction(async ()=>await imgDoubleConfirmTitle.HideAsync());
			btnSubmitTitle.AddAwaitAction(async () =>
			{
				await titleGroup.HideAsync();
				await imgPlay.ShowAsync();
			});
			//是否进入整体流程视频页面
			btnConfirmPlay.AddAwaitAction(async () =>
			{
				await imgPlay.HideAsync();
				await vpVideo.ShowAsync();
				vpVideo.Play();
			});
			btnCancelPlay.AddAwaitAction(async () =>
			{
				await imgPlay.HideAsync();
				await objCraft.ShowAsync();
			});
			//整体流程视频页面
			btnCloseVideo.AddAwaitAction(async () =>
			{
				vpVideo.Stop();
				await this.HideAsyncPanel();
				await UIKit.GetPanel<MainPanel>().ShowAsyncPanel();
			});
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			objCraft.ShowSync();
			imgAnimationEnd.HideSync();
			titleGroup.HideSync();
			imgAnimation.HideSync();
			imgPlay.HideSync();
			vpVideo.HideSync();
			imgDoubleConfirmTitle.HideSync();
			foreach (var craftItem in craftRess)
			{
				if (craftItem.tog.isOn)
					craftItem.tog.isOn = false;
			}
			craftRess[0].tog.isOn = true;
			tmpCraftName.text = "木材的"+craftRess[0].craftType.GetDescription();
			tmpCraftDescription.text = craftRess[0].description;
			//资源状态重置
			selectedIndex = 0;
			btnSubmitTitle.gameObject.SetActive(false);
		}

		protected override void OnHide()
		{
			GameManager.Instance.EndLearnAnimation(craftRess[selectedIndex].craftType);
		}

		protected override void OnClose()
		{
		}
	}
}
