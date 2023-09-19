using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using HighlightPlus;
using QFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectBase
{
    public static class ExtensionFunction
    {
        public static float HideTime = 0.5f;
        public static float ShowTime = 0.5f;
        
        #region UI扩展

        static void LoadUIAsync(PanelSearchKeys panelSearchKeys, Action<IPanel> onLoad)
        {
            var retPanel = UIKit.Table.GetPanelsByPanelSearchKeys(panelSearchKeys).FirstOrDefault();

            if (retPanel == null)
            {
                UIManager.Instance.CreateUIAsync(panelSearchKeys, (panel) =>
                {
                    retPanel = panel;
                    onLoad?.Invoke(retPanel);
                });
            }
        }

        /// <summary>
        ///  预加载UI，不会触发OnShow和OnInit
        /// </summary>
        /// <param name="canvasLevel"></param>
        /// <param name="uiData"></param>
        /// <param name="assetBundleName"></param>
        /// <param name="prefabName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerator PreLoadPanelAsync<T>(UILevel canvasLevel = UILevel.Common, IUIData uiData = null,
            string assetBundleName = null,
            string prefabName = null) where T : UIPanel
        {
            var panelSearchKeys = PanelSearchKeys.Allocate();
            panelSearchKeys.OpenType = PanelOpenType.Single;
            panelSearchKeys.Level = canvasLevel;
            panelSearchKeys.PanelType = typeof(T);
            panelSearchKeys.AssetBundleName = assetBundleName;
            panelSearchKeys.GameObjName = prefabName;
            panelSearchKeys.UIData = null;
            bool loaded = false;

            LoadUIAsync(panelSearchKeys, panel => { loaded = true; });

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (!loaded)
            {
                yield return wait;
            }

            panelSearchKeys.Recycle2Cache();
        }
        
        public static Func<bool> GetAnimatorEndFunc(this Button btn)
        {
            Animator animator = btn.animator;
            return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
        }

        public static Func<bool> GetAnimatorEndFunc(this Toggle tog)
        {
            Animator animator = tog.animator;
            return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
        }

        //智力障碍症学生：说话、坐下、站起、站立着低头、坐着使用电脑
        //孤独症学生：说话、坐下、坐着低头、坐着画画

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,但是本Button的动画会和异步函数同时执行
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Button btn, Func<UniTask> invoke)
        {
            CancellationToken token = btn.GetCancellationTokenOnDestroy();
            UnityAction asyncAction = async () =>
            {
                if (token.IsCancellationRequested) return;
                UIRoot.Instance.GraphicRaycaster.enabled = false;
                await invoke();
                UIRoot.Instance.GraphicRaycaster.enabled = true;
            };
            btn.onClick.AddListener(asyncAction);
        }
        
        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,但是本Button的动画会和异步函数同时执行
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Button btn, Action invoke)
        {
            Func<bool> animFunc = btn.GetAnimatorEndFunc();
            CancellationToken token = btn.GetCancellationTokenOnDestroy();
            UnityAction asyncAction = async () =>
            {
                if (token.IsCancellationRequested) return;
                UIRoot.Instance.GraphicRaycaster.enabled = false;
                await UniTask.WaitUntil(animFunc);
                invoke();
                UIRoot.Instance.GraphicRaycaster.enabled = true;
            };
            btn.onClick.AddListener(asyncAction);
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,Toggle动画执行完成后才会执行异步事件
        /// </summary>
        /// <param name="tog"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Toggle tog, Action<bool> invoke)
        {
            CancellationToken token = tog.GetCancellationTokenOnDestroy();
            UnityAction<bool> asyncAction = null;
            //有group的情况下，会同时触发两个toggle，因此屏蔽由isOn的Toggle管理。
            if (tog.group != null && !tog.group.allowSwitchOff)
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await tog.animator.GetAsyncAnimatorMoveTrigger().FirstAsync(tog.GetCancellationTokenOnDestroy());
                    invoke(isOn);
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }
            else
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await tog.animator.GetAsyncAnimatorMoveTrigger().FirstAsync(tog.GetCancellationTokenOnDestroy());
                    invoke(isOn);
                    UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }

            tog.onValueChanged.AddListener(asyncAction);
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,Toggle动画会与异步事件同时执行
        /// </summary>
        /// <param name="tog"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Toggle tog, Func<bool, UniTask> invoke)
        {
            CancellationToken token = tog.GetCancellationTokenOnDestroy();
            UnityAction<bool> asyncAction = null;
            //有group的情况下，会同时触发两个toggle，因此屏蔽由isOn的Toggle管理。
            if (tog.group != null && !tog.group.allowSwitchOff)
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await invoke(isOn);
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }
            else
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await invoke(isOn);
                    UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }

            tog.onValueChanged.AddListener(asyncAction);
        }

        public static async UniTask ShowAsync(this UIPanel panel)
        {
            panel.Show();
            await panel.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask HideAsync(this UIPanel panel)
        {
            await panel.transform.DOLocalMoveY(1080, ShowTime).AsyncWaitForCompletion();
            panel.Hide();
        }
        
        public static Func<UniTask> GetShowAsync(this Component component,Vector3 showPos)
        {
            return async () =>
            {
                component.gameObject.SetActive(true);
                await component.transform.DOLocalMove(showPos, HideTime).AsyncWaitForCompletion();
            };
        }
        
        public static async UniTask ShowAsync(this Component component)
        {
            component.gameObject.SetActive(true);
            await component.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }
        
        public static async UniTask ShowAsync(this UIElement component)
        {
            component.Show();
            await component.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }
        
        public static async UniTask ShowAwait(this Component component)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            component.gameObject.SetActive(true);
            await component.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
            UIRoot.Instance.GraphicRaycaster.enabled = true;
        }
        
        public static void ShowSync(this UIElement uiElement)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            uiElement.gameObject.SetActive(true);
            uiElement.transform.DOLocalMoveY(0, HideTime).OnComplete(() =>
            {
                uiElement.Show();
                UIRoot.Instance.GraphicRaycaster.enabled = true;
            });
        }
        
        public static async UniTask HideAsync(this Component component)
        {
            await component.transform.DOLocalMoveY(1080, HideTime).AsyncWaitForCompletion();
            component.gameObject.SetActive(false);
        }

        public static async UniTask HideAsync(this UIElement component)
        {
            await component.transform.DOLocalMoveY(1080, HideTime).AsyncWaitForCompletion();
            component.gameObject.Hide();
        }
        
        public static async UniTask HideAwait(this Component component)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            await component.transform.DOLocalMoveY(1080, HideTime).AsyncWaitForCompletion();
            component.gameObject.SetActive(false);
            UIRoot.Instance.GraphicRaycaster.enabled = true;
        }
        
        public static async void HideSync(this UIElement uiElement)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            uiElement.transform.DOLocalMoveY(1080, HideTime).OnComplete(() =>
            {
                uiElement.Hide();
                UIRoot.Instance.GraphicRaycaster.enabled = true;
            });
        }

        public static Func<UniTask> GetHideAsync(this Component component,Vector3 hidePos)
        {
            return async () =>
            {
                await component.transform.DOLocalMove(hidePos, HideTime).AsyncWaitForCompletion();
                component.gameObject.SetActive(false);
            };
        }
        
        public static void ShowSync(this Component component)
        {
            component.gameObject.SetActive(true);
            (component.transform as RectTransform).anchoredPosition =  Vector3.zero;
        }

        public static void HideSync(this Component component)
        {
            (component.transform as RectTransform).anchoredPosition =  new Vector3(0, 1080, 0);
            component.gameObject.SetActive(false);
        }
        
        public static async UniTask PlayAsync(this Animator animator, string clipName, CancellationToken token)
        {
            CancellationTokenSource link =
                CancellationTokenSource.CreateLinkedTokenSource(token, animator.GetCancellationTokenOnDestroy());
            if (link.IsCancellationRequested)
                return;
            animator.Play(clipName);
            await animator.GetAsyncAnimatorMoveTrigger().FirstOrDefaultAsync(link.Token);
        }
        
        #endregion


        #region 3D物体扩展

        public static async UniTask HightlightClickAsync(this GameObject obj, CancellationToken cancellationToken)
        {
            HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
            if (highlightEffect == null)
                highlightEffect = obj.AddComponent<HighlightEffect>();
            highlightEffect.highlighted = true;
            highlightEffect.outlineColor = Color.red;

            await obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(cancellationToken);
            highlightEffect.highlighted = false;
        }

        public static async UniTask HightlightClickAsync(this List<GameObject> objs,
            CancellationToken cancellationToken, Action<GameObject> callBack = null)
        {
            List<HighlightEffect> objsHighlight = new List<HighlightEffect>();
            int count = objs.Count;
            for (int i = 0; i < objs.Count; i++)
            {
                GameObject obj = objs[i];
                HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
                if (highlightEffect == null)
                    highlightEffect = obj.AddComponent<HighlightEffect>();
                highlightEffect.highlighted = true;
                highlightEffect.outlineColor = Color.red;
                objsHighlight.Add(highlightEffect);
                obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(d =>
                {
                    highlightEffect.highlighted = false;
                    count--;
                    callBack?.Invoke(obj);
                    return true;
                }, cancellationToken).Forget();
            }

            await UniTask.WaitUntil(() => count == 0, cancellationToken: cancellationToken);
        }

        #endregion

        #region 查找

        public static Transform FindByTag(this Transform parent, string tag)
        {
            return parent.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.CompareTag(tag));
        }
        #endregion
    }
}