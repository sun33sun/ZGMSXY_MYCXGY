using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using ProjectBase;
using TMPro;

namespace ZGMSXY_MYCXGY
{
    public class InteractionPanelData : UIPanelData
    {
    }

    public partial class InteractionPanel : UIPanel
    {
        List<GameObject> groups = new List<GameObject>();
        private CancellationTokenSource cts;

        protected override void OnInit(IUIData uiData = null)
        {
            cts = new CancellationTokenSource();
            mData = uiData as InteractionPanelData ?? new InteractionPanelData();

            btnBack.AddAwaitAction(async () =>
            {
                await this.HideAsync();
                await UIKit.GetPanel<MainPanel>().ShowAsync();
            });

            btnConfirmNext.AddAwaitAction(async () => await imgNext.HideAsync());

            btnConfirmPlayRealVideo.AddAwaitAction(async () =>
            {
                await imgPlayRealVideo.HideAsync();
                vpRealVideo.gameObject.SetActive(true);
                vpRealVideo.Play();
            });

            btnCancelPlayRealVideo.AddAwaitAction(async () =>
            {
                await imgPlayRealVideo.HideAsync();
                await imgSubmitModel.ShowAsync();
            });

            btnConfirmSubmitModel.AddAwaitAction(async () =>
            {
                await this.HideAsync();
                await UIKit.GetPanel<EvaluatePanel>().ShowAsync();
            });

            btnCancelSubmitModel.AddAwaitAction(async () =>await imgSubmitModel.HideAsync());
        }

        async UniTaskVoid SubscribeUIElements()
        {
            await SelectMaterial.btnConfirmMaterial.OnClickAsync();
            await SelectCase.ShowAwait();
            await SelectCase.btnConfirmMaterial.OnClickAsync();
            GameManager.Instance.StartTask(SelectMaterial.nowMaterialType, SelectCase.nowCaseType);
        }
        
        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            UIKit.GetPanel<MainPanel>().imgBk.enabled = false;
            CameraManager.Instance.SwitchRenderTexture(false);
            cts = new CancellationTokenSource();
            vpRealVideo.gameObject.SetActive(false);
            imgNext.HideSync();
            imgPlayRealVideo.HideSync();
            imgSubmitModel.HideSync();
            btnEnterNext.gameObject.SetActive(false);
            SelectMaterial.ShowSync();
            SelectCase.HideSync();
            toolSelections.HideSync();
            SubscribeUIElements().Forget();
        }

        protected override void OnHide()
        {
            UIKit.GetPanel<MainPanel>().imgBk.enabled = true;
            vpRealVideo.Stop();
            GameManager.Instance.CancelTask?.Invoke();
            cts.Cancel();
        }

        protected override void OnClose()
        {
        }

        public void NextState()
        {
            taskSchedule.NextState();
        }

        public void InitTaskState(List<string> taskList)
        {
            taskSchedule.InitTaskState(taskList);
        }

        public void ShowFinishProduct()
        {
            ShowAsyncFinishProduct().Forget();
        }

        async UniTaskVoid ShowAsyncFinishProduct()
        {
            // UIKit.GetPanel<TopPanel>().tmpTip.text = "您可再次选择材料，以改变最终成品";
            // await SelectMaterial.ShowAsync();
            // await SelectMaterial.btnConfirmMaterial.OnClickAsync(cts.Token);

            TextMeshProUGUI tmpTip = UIKit.GetPanel<TopPanel>().tmpTip;
            tmpTip.text = "仿真动画播放完毕，点击右下角按钮进入下一步";
            await toolSelections.HideAwait();
            btnEnterNext.gameObject.SetActive(true);
            await btnEnterNext.OnClickAsync(cts.Token);
            await imgPlayRealVideo.ShowAwait();
            int anyIndex = await UniTask.WhenAny(btnConfirmPlayRealVideo.OnClickAsync(cts.Token),
                btnCancelPlayRealVideo.OnClickAsync(cts.Token));
            if (anyIndex == 0)
            {
                tmpTip.text = "观看真人实操视频";
                btnEnterNext.gameObject.SetActive(false);
                await UniTask.Yield(cancellationToken:cts.Token);
                await UniTask.WaitUntil(() => !vpRealVideo.isPlaying,
                    cancellationToken: cts.Token);
                btnEnterNext.gameObject.SetActive(true);
                vpRealVideo.gameObject.SetActive(false);
                await imgSubmitModel.ShowAsync();
            }
            tmpTip.text = "点击“是”或右下角按钮查看他人模型";
            anyIndex = await UniTask.WhenAny(btnConfirmSubmitModel.OnClickAsync(cts.Token),btnCancelSubmitModel.OnClickAsync(cts.Token));
            if (anyIndex == 0)
            {
                await this.HideAsync();
                await UIKit.GetPanel<EvaluatePanel>().ShowAsync();
            }
            else
            {
                await btnEnterNext.OnClickAsync(cts.Token);
                await this.HideAsync();
                await UIKit.GetPanel<EvaluatePanel>().ShowAsync();
            }
        }

        public async UniTask WaitNext(CancellationToken token)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            await imgNext.ShowAsync();
            UIRoot.Instance.GraphicRaycaster.enabled = true;
            await btnConfirmNext.OnClickAsync(token);
        }
    }
}