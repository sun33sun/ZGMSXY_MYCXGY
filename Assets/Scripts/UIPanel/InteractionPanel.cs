using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public class InteractionPanelData : UIPanelData
    {
        public SelectMaterial.MaterialType MaterialType;
        public SelectCase.CaseType CaseType;
    }

    public partial class InteractionPanel : UIPanel
    {
        List<GameObject> groups = new List<GameObject>();
        private CancellationTokenSource cts;
        

        protected override void OnInit(IUIData uiData = null)
        {
            InitGroups();
            cts = new CancellationTokenSource();
            mData = uiData as InteractionPanelData ?? new InteractionPanelData();

            btnBack.AddAwaitAction(async () =>
            {
                await this.HideAsyncPanel();
                await UIKit.GetPanel<MainPanel>().ShowAsyncPanel();
            });

            // btnCancelNext.AddAwaitAction(async () =>
            // {
            //     materialGroup.transform.DOLocalMoveY(-800, ExtensionFunction.ShowTime);
            //     await imgNext.HideAsync();
            //     materialGroup.gameObject.SetActive(false);
            //     NextState();
            //     GameManager.Instance.gameObject.SetActive(true);
            //     UniTask.Void(async t =>
            //     {
            //         await GameManager.Instance.WaitClickCube();
            //         imgPlayRealVideo.ShowAsync().Forget();
            //     }, cts.Token);
            // });
            
            btnConfirmNext.AddAwaitAction(async () =>await imgNext.HideAsync());

            btnConfirmPlayRealVideo.AddAwaitAction(async () =>
            {
                await imgPlayRealVideo.HideAsync();
                NextState();
                vpRealVideo.gameObject.SetActive(true);
                vpRealVideo.Play();
                await UniTask.Yield(PlayerLoopTiming.Update);
                UniTask.Void(async t =>
                {
                    await UniTask.WaitUntil(() => !vpRealVideo.isPlaying,
                        cancellationToken: this.GetCancellationTokenOnDestroy());
                    vpRealVideo.gameObject.SetActive(false);
                    imgSubmitModel.ShowAsync().Forget();
                }, this);
            });

            btnCancelPlayRealVideo.AddAwaitAction(async () =>
            {
                await imgPlayRealVideo.HideAsync();
                NextState();
                await imgSubmitModel.ShowAsync();
            });

            btnConfirmSubmitModel.AddAwaitAction(async () =>
            {
                await this.HideAsyncPanel();
                await UIKit.GetPanel<EvaluatePanel>().ShowAsyncPanel();
            });

            btnCancelSubmitModel.AddAwaitAction(async () =>
            {
                await imgSubmitModel.HideAsync();
                btnEnterEvaluate.gameObject.SetActive(true);
            });

            btnEnterEvaluate.AddAwaitAction(async () =>
            {
                await imgSubmitModel.ShowAsync();
                btnEnterEvaluate.gameObject.SetActive(false);
            });

            SubscribeUIElements();
        }

        void SubscribeUIElements()
        {
            SelectMaterial.OnConfirmMaterial += selectedMaterial =>
            {
                mData.MaterialType = selectedMaterial;
                SelectCase.ShowAwaitUIElement();
            };
            SelectCase.OnConfirmCase += selectedCase =>
            {
                mData.CaseType = selectedCase;
                toolSelections.ShowAwaitUIElement();
                GameManager.Instance.StartTask(mData.MaterialType,mData.CaseType);
            };
        }
        
        void InitGroups()
        {
            // groups.Add(materialGroup.gameObject);
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
            btnEnterEvaluate.gameObject.SetActive(false);
            SelectMaterial.ShowSync();
            SelectCase.HideSync();
            toolSelections.HideSync();
        }

        protected override void OnHide()
        {
            UIKit.GetPanel<MainPanel>().imgBk.enabled = true;
            vpRealVideo.Stop();
            cts.Cancel();
        }

        protected override void OnClose()
        {
        }

        public void NextState()
        {
            taskSchedule.NextState();
        }

        public async UniTask WaitNext()
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            await imgNext.ShowAsync();
            UIRoot.Instance.GraphicRaycaster.enabled = true;
            await btnConfirmNext.OnClickAsync(cts.Token);
        }
    }
}