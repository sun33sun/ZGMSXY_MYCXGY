using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using ProjectBase;
using QFramework;

namespace ZGMSXY_MYCXGY
{
    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        [SerializeField] GameLibrary library;
        CancellationTokenSource ctsDisable = null;
        private Action cancelTask = null;
        public Action CancelTask => cancelTask;

        protected override void Awake()
        {
            base.Awake();
            library.ModelParent.SetActive(false);
        }

        private void OnEnable()
        {
            ctsDisable = new CancellationTokenSource();
        }

        public void StartLearnAnimation(CraftRes.CraftType craftType)
        {
            library.ModelParent.SetActive(true);
            GameObject targetInstance = library.GetLearnGameObject(craftType);
            CameraManager.Instance.nowC.Follow = targetInstance.transform;
            CameraManager.Instance.SwitchRenderTexture(true);
        }

        public void EndLearnAnimation(CraftRes.CraftType craftType)
        {
            library.DestroyLearnGameObject(craftType);
            CameraManager.Instance.SwitchRenderTexture(false);
        }

        private void OnDisable()
        {
            ctsDisable?.Cancel();
        }

        public void StartTask(SelectMaterial.MaterialType materialType, SelectCase.CaseType caseType)
        {
            library.ModelParent.gameObject.SetActive(true);
            switch (materialType)
            {
                case SelectMaterial.MaterialType.m0:
                    switch (caseType)
                    {
                        case SelectCase.CaseType.c0:
                            TaskInteractive1 _taskInteractive1 = Instantiate(library.TaskInteractive1,
                                library.ModelParent.transform, false);
                            _taskInteractive1.StartTask(library);
                            cancelTask = () => Destroy(_taskInteractive1.gameObject);
                            break;
                        case SelectCase.CaseType.c1:
                            TaskInteractive2 _taskInteractive2 = Instantiate(library.TaskInteractive2,
                                library.ModelParent.transform, false);
                            _taskInteractive2.StartTask(library);
                            cancelTask = () => Destroy(_taskInteractive2.gameObject);
                            break;
                    }

                    break;
            }
        }
    }
}