using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
    public partial class TaskInteractive2 : ViewController
    {
        private GameLibrary _library;
        InteractionPanel _interactionPanel;
        private ToolSelections _toolSelections;
        private CancellationToken _token;
        private TopPanel _topPanel;

        private List<string> taskList = new List<string>()
        {
            "选择原料",
            "模型标记",
            "模型切割",
            "车身钻铣",
            "车轮制作",
            "车身切割与打磨",
            "车体组装",
        };

        private void Awake()
        {
            _token = this.GetCancellationTokenOnDestroy();
        }

        Func<bool> GetAnimEndFunc(Animator animator, string stateName)
        {
            return () =>
            {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                return info.IsName(stateName) && info.normalizedTime > 1;
            };
        }

        public void StartTask(GameLibrary library)
        {
            _library = library;
            _interactionPanel = UIKit.GetPanel<InteractionPanel>();
            _interactionPanel.InitTaskState(taskList);            
            _toolSelections = _interactionPanel.toolSelections;
            _topPanel = UIKit.GetPanel<TopPanel>();
            // DrawLine().Forget();
            
            FixAxleAndWheel().Forget();
        }

        async UniTaskVoid DrawLine()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用尺子和笔在材料上画线";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Ruler, ToolSelections.Tool.Pencil);
            Animator animatorDrawLine = Instantiate(Task2_DrawLine, transform, false);
            CameraManager.Instance.FollowPersonView(animatorDrawLine.transform);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrawLine, "Task2_DrawLine4"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrawLine.gameObject);
            Bandcut().Forget();
        }

        async UniTaskVoid Bandcut()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用带锯将多余部分切除";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Bandcut);
            Animator animatorBandcut = Instantiate(Task2_Bandcut, transform, false);
            CameraManager.Instance.nowC.Follow = animatorBandcut.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorBandcut, "Task2_Bandcut1"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorBandcut.gameObject);
            Drilling().Forget();
        }

        async UniTaskVoid Drilling()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "交替使用钻头钻出安装车轮的缺口";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Bit_Fine, ToolSelections.Tool.Bit_Coarse,
                ToolSelections.Tool.DrillAndMillingMachine);
            Animator animatorDrilling = Instantiate(Task2_Drilling, transform, false);
            CameraManager.Instance.nowC.Follow = animatorDrilling.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrilling, "Task2_Drilling2"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrilling.gameObject);
            AxleAndWheel().Forget();
        }

        async UniTaskVoid AxleAndWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用钻头和带锯制作车轮";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.DrillAndMillingMachine,
                ToolSelections.Tool.Bit_Fine, ToolSelections.Tool.Bandcut);
            Animator animatorAxle = Instantiate(Task2_AxleAndWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorAxle.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorAxle, "Task2_AxleAndWheel8"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorAxle.gameObject);
            BandcutCar().Forget();
        }

        async UniTaskVoid BandcutCar()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用带锯，沿标记线切出车身。然后使用打磨机、锉刀、打磨棒依次打磨车身";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Bandcut, ToolSelections.Tool.Sander,
                ToolSelections.Tool.File_File, ToolSelections.Tool.File_Coarse, ToolSelections.Tool.SanderStick);
            Animator animatorBandcut = Instantiate(Task2_BandcutCar, transform, false);
            CameraManager.Instance.nowC.Follow = animatorBandcut.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorBandcut, "Task2_BandcutCar3"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorBandcut.gameObject);
            FixAxleAndWheel().Forget();
        }

        async UniTaskVoid FixAxleAndWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "组合车轮、车轴和车架";
            Animator animatorFixAxleAndWheel = Instantiate(Task2_FixAxleAndWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorFixAxleAndWheel.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorFixAxleAndWheel, "Task2_FixAxleAndWheel2"),
                cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorFixAxleAndWheel.gameObject);
            _interactionPanel.ShowFinishProduct();
        }
    }
}