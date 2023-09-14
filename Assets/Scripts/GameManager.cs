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
        private TaskInteractive1 _taskInteractive1 = null;


        protected override void Awake()
        {
            base.Awake();
            library.Cube.gameObject.OnPointerEnterEvent(d => library.imgTip.gameObject.SetActive(true));
            library.Cube.gameObject.OnPointerExitEvent(d => library.imgTip.gameObject.SetActive(true));
            library.ModelParent.SetActive(false);
        }

        private void OnEnable()
        {
            ctsDisable = new CancellationTokenSource();
        }

        private void Start()
        {
            // library.StartTableSawSliceWood();
            // library.StartBandSawSliceWood();
            // library.StartDrillDrillingWood();
            // library.StartHobbingDrillingWood();
            // library.StartSanderPolishWood();
        }

        public void StartLearnAnimation(CraftRes.CraftType craftType)
        {
            library.ModelParent.SetActive(true);
            GameObject targetInstance = library.GetLearnGameObject(craftType);
            CameraManager.Instance.nowC.Follow = targetInstance.transform;
        }

        public void EndLearnAnimation(CraftRes.CraftType craftType)
        {
            library.DestroyLearnGameObject(craftType);
        }

        private void OnDisable()
        {
            ctsDisable?.Cancel();
        }

        public async UniTask WaitClickCube()
        {
            library.Cube.gameObject.SetActive(true);
            await library.Cube.triggers.Find(t => t.eventID == EventTriggerType.PointerClick).callback
                .OnInvokeAsync(ctsDisable.Token);
            library.Cube.gameObject.SetActive(false);
            library.imgTip.gameObject.SetActive(false);
        }

        public void StartTask(SelectMaterial.MaterialType materialType, SelectCase.CaseType caseType)
        {
            InteractionPanel panel = UIKit.GetPanel<InteractionPanel>();
            library.ModelParent.gameObject.SetActive(true);
            switch (materialType)
            {
                case SelectMaterial.MaterialType.m0:
                    switch (caseType)
                    {
                        case SelectCase.CaseType.c0:
                            _taskInteractive1 = Instantiate(library.TaskInteractive1, library.ModelParent.transform);
                            _taskInteractive1.transform.position = Vector3.zero;
                            _taskInteractive1.StartTask(library, panel, panel.toolSelections);
                            break;
                    }

                    break;
            }
        }
    }
}