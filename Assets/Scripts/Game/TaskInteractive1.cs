using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ProjectBase;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ZGMSXY_MYCXGY
{
    public partial class TaskInteractive1 : ViewController
    {
        private GameLibrary _library;
        InteractionPanel _interactionPanel;
        private ToolSelections _toolSelections;
        private CancellationToken _token;
        private TopPanel _topPanel;

        private List<string> taskList = new List<string>()
        {
            "选择原料",
            "模型标注",
            "车身钻铣",
            "车身切割",
            "车轮制作",
            "车轮打磨",
            "部件安装",
            "车身打磨"
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
            
            DrawLine().Forget();
        }

        async UniTaskVoid DrawLine()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用尺子和笔在材料上画线";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Ruler, ToolSelections.Tool.Pencil);
            Animator animatorDrawLine = Instantiate(Task1_DrawLine, transform, false);
            CameraManager.Instance.FollowPersonView(animatorDrawLine.transform);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrawLine, "Measure2"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrawLine.gameObject);
            Drilling().Forget();
        }

        async UniTaskVoid Drilling()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "交替使用钻头在车身画线处钻出容纳车轮的孔洞";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.DrillAndMillingMachine,
                ToolSelections.Tool.Bit_Fine, ToolSelections.Tool.Bit_Coarse);
            Animator animatorDrilling = Instantiate(Task1_Drilling, transform, false);
            CameraManager.Instance.nowC.Follow = GameObject.FindWithTag("RoundPoint").transform;
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrilling, "Task1_Drilling4"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrilling.gameObject);
            Bandcut().Forget();
        }

        async UniTaskVoid Bandcut()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用带锯将多余部分切除";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Bandcut);
            Animator animatorBandcut = Instantiate(Task1_Bandcut, transform, false);
            CameraManager.Instance.nowC.Follow = animatorBandcut.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorBandcut, "Task1_Bandcut"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorBandcut.gameObject);
            AxleAndWheel().Forget();
        }

        async UniTaskVoid AxleAndWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用打孔机和切割机制作车轮";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.DrillAndMillingMachine,
                ToolSelections.Tool.Bandcut, ToolSelections.Tool.Bit_Fine);
            Animator animatorAxleAndWheel = Instantiate(Task1_AxleAndWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorAxleAndWheel.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorAxleAndWheel, "Task1_AxleAndWheel8"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.FollowPersonView(null);
            Destroy(animatorAxleAndWheel.gameObject);
            PolishWheel().Forget();
        }

        async UniTaskVoid PolishWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用打磨机打磨车轮";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Sander);
            Animator animatorPolishWheel = Instantiate(Task1_PolishWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorPolishWheel.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorPolishWheel, "Sander_Wheel4"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorPolishWheel.gameObject);
            FixWheelAndAxle().Forget();
        }

        async UniTaskVoid FixWheelAndAxle()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "将车轴和车轮安装到车身上，然后打磨车身";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.Sander);
            Animator animatorFixWheelAndAxle = Instantiate(Task1_FixWheelAndAxle, transform, false);
            CameraManager.Instance.nowC.Follow = animatorFixWheelAndAxle.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorFixWheelAndAxle, "Task1_FixWheelAndAxle2"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorFixWheelAndAxle);
            PolishCarInner().Forget();
        }

        async UniTaskVoid PolishCarInner()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            _topPanel.tmpTip.text = "使用打磨棒将车身内侧打磨光滑";
            await _toolSelections.WaitSelectCorrectTool(_token, ToolSelections.Tool.SanderStick);
            Animator animatorPolishCarInner = Instantiate(Task1_PolishCarInner, transform, false);
            CameraManager.Instance.nowC.Follow = animatorPolishCarInner.transform.FindByTag("RoundPoint");
            await UniTask.WaitUntil(GetAnimEndFunc(animatorPolishCarInner, "Task1_PolishCarInner1"),cancellationToken:_token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorPolishCarInner.gameObject);
            _interactionPanel.NextState();
            _interactionPanel.ShowFinishProduct();
        }
    }
}