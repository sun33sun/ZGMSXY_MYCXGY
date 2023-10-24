using System;
using System.Collections;
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

        string strTag = "RoundPoint";


		private List<string> taskList = new List<string>()
        {
            TaskName.SelectMaterial.Description(),
            TaskName.ModelMark.Description(),
            TaskName.BodyDrill.Description(),
            TaskName.ModelCut.Description(),
            TaskName.WheelMaking.Description(),
            TaskName.WheelPolish.Description(),
            TaskName.PartInstall.Description(),
            TaskName.BodyPolish.Description(),
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

            CameraManager.Instance.nowC.m_Lens.FieldOfView = 60;
            //DrawLine().Forget();

            PolishCarInner().Forget();
        }

        async UniTaskVoid DrawLine()
        {
            _interactionPanel.NextState();
            System.GC.Collect();

            ToolName[] tools = { ToolName.Ruler, ToolName.Pencil };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}在材料上画线";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);
            
            Animator animatorDrawLine = Instantiate(Task1_DrawLine, transform, false);
            CameraManager.Instance.FollowPersonView(animatorDrawLine.transform);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrawLine, "Measure2"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrawLine.gameObject);
            Drilling().Forget();
        }

        async UniTaskVoid Drilling()
        {
            _interactionPanel.NextState();
            System.GC.Collect();

            ToolName[] tools = { ToolName.DrillAndMillingMachine, ToolName.Bit_Fine, ToolName.Bit_Coarse };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}在车身画线处钻出容纳车轮的孔洞";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);
            
            Animator animatorDrilling = Instantiate(Task1_Drilling, transform, false);
            CameraManager.Instance.nowC.Follow = GameObject.FindWithTag(strTag).transform;
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrilling, "Task1_Drilling4"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorDrilling.gameObject);
            Bandcut().Forget();
        }

        async UniTaskVoid Bandcut()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            
            ToolName[] tools = { ToolName.Bandcut };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}切除多余部分";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);            

            Animator animatorBandcut = Instantiate(Task1_Bandcut, transform, false);
            CameraManager.Instance.nowC.Follow = animatorBandcut.transform.FindByTag(strTag);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorBandcut, "Task1_Bandcut"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorBandcut.gameObject);
            AxleAndWheel().Forget();
        }

        async UniTaskVoid AxleAndWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();
            
            
            ToolName[] tools = { ToolName.DrillAndMillingMachine, ToolName.Bandcut, ToolName.Bit_Fine };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}制作车轮";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);
            
            Animator animatorAxleAndWheel = Instantiate(Task1_AxleAndWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorAxleAndWheel.transform.FindByTag(strTag);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorAxleAndWheel, "Task1_AxleAndWheel8"),
                cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorAxleAndWheel.gameObject);
            PolishWheel().Forget();
        }

        async UniTaskVoid PolishWheel()
        {
            _interactionPanel.NextState();
            System.GC.Collect();

            ToolName[] tools = { ToolName.Sander };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}打磨车轮";
            await _toolSelections.WaitSelectCorrectTool(_token,tools );
            
            Animator animatorPolishWheel = Instantiate(Task1_PolishWheel, transform, false);
            CameraManager.Instance.nowC.Follow = animatorPolishWheel.transform.FindByTag(strTag);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorPolishWheel, "Sander_Wheel4"), cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorPolishWheel.gameObject);
            FixWheelAndAxle().Forget();
        }

        async UniTaskVoid FixWheelAndAxle()
        {
            _interactionPanel.NextState();
            System.GC.Collect();

            ToolName[] tools = { ToolName.Sander };
            _topPanel.tmpTip.text = $"将车轴和车轮安装到车身上，然后使用{tools.Descriptions()}打磨车身";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);
            
            Animator animatorFixWheelAndAxle = Instantiate(Task1_FixWheelAndAxle, transform, false);
            CameraManager.Instance.nowC.Follow = animatorFixWheelAndAxle.transform.FindByTag(strTag);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorFixWheelAndAxle, "Task1_FixWheelAndAxle2"),
                cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorFixWheelAndAxle.gameObject);
            PolishCarInner().Forget();
        }

        async UniTaskVoid PolishCarInner()
        {
            _interactionPanel.NextState();
            System.GC.Collect();

            ToolName[] tools = { ToolName.SanderStick };
            _topPanel.tmpTip.text = $"使用{tools.Descriptions()}将车身内侧打磨光滑";
            await _toolSelections.WaitSelectCorrectTool(_token, tools);
           
            Animator animatorPolishCarInner = Instantiate(Task1_PolishCarInner, transform, false);
            CameraManager.Instance.nowC.Follow = animatorPolishCarInner.transform.FindByTag(strTag);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorPolishCarInner, "Task1_PolishCarInner1"),
                cancellationToken: _token);
            await _interactionPanel.WaitNext(_token);
            CameraManager.Instance.nowC.Follow = null;
            Destroy(animatorPolishCarInner.gameObject);
            _interactionPanel.NextState();
            _interactionPanel.ShowFinishProduct();
        }
    }
}