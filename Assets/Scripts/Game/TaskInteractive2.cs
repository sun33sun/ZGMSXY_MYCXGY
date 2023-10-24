using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public enum TaskName
	{
		[Description("选择材料")] SelectMaterial,
		[Description("模型标注")] ModelMark,
		[Description("模型切割")] ModelCut,
		[Description("车身钻铣")] BodyDrill,
		[Description("车轮制作")] WheelMaking,
		[Description("车身制作与打磨")] BodyMakingAndPolish,
		[Description("部件安装")] PartInstall,
		[Description("车轮打磨")] WheelPolish,
		[Description("车身打磨")] BodyPolish
	}

	public partial class TaskInteractive2 : ViewController
	{
		private GameLibrary _library;
		InteractionPanel _interactionPanel;
		private ToolSelections _toolSelections;
		private CancellationToken _token;
		private TopPanel _topPanel;

		private List<string> taskList = new List<string>()
		{
			TaskName.SelectMaterial.Description(),
			TaskName.ModelMark.Description(),
			TaskName.ModelCut.Description(),
			TaskName.BodyDrill.Description(),
			TaskName.WheelMaking.Description(),
			TaskName.BodyMakingAndPolish.Description(),
			TaskName.PartInstall.Description(),
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

			ToolName[] tools = { ToolName.Ruler, ToolName.Pencil };
			_topPanel.tmpTip.text = $"使用{tools.Descriptions()}在材料上画线";
			await _toolSelections.WaitSelectCorrectTool(_token, tools);

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

			ToolName[] tools = { ToolName.Bandcut };
			_topPanel.tmpTip.text = $"使用{tools.Descriptions()}将多余部分切除";
			await _toolSelections.WaitSelectCorrectTool(_token, tools);

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

			ToolName[] tools = { ToolName.Bit_Fine, ToolName.Bit_Coarse, ToolName.DrillAndMillingMachine };
			_topPanel.tmpTip.text = $"使用{tools.Descriptions()}钻出安装车轮的缺口";
			await _toolSelections.WaitSelectCorrectTool(_token, tools);

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

			ToolName[] tools = { ToolName.DrillAndMillingMachine, ToolName.Bit_Fine, ToolName.Bandcut };
			_topPanel.tmpTip.text = $"使用{tools.Descriptions()}制作车轮";
			await _toolSelections.WaitSelectCorrectTool(_token, tools);

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

			ToolName[] tools =
				{ ToolName.Bandcut, ToolName.Sander, ToolName.File_File, ToolName.File_Coarse, ToolName.SanderStick };
			_topPanel.tmpTip.text =
				$"使用{ToolName.Bandcut.Description()}沿标记线切出车身,然后使用{new[] { ToolName.Sander, ToolName.File_File, ToolName.File_Coarse, ToolName.SanderStick }.Descriptions()}依次打磨车身";
			await _toolSelections.WaitSelectCorrectTool(_token, tools);

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
			await UniTask.WaitUntil(GetAnimEndFunc(animatorFixAxleAndWheel, "Task2_FixAxleAndWheel1"),
				cancellationToken: _token);
			await _interactionPanel.WaitNext(_token);
			CameraManager.Instance.nowC.Follow = null;
			Destroy(animatorFixAxleAndWheel.gameObject);
			_interactionPanel.ShowFinishProduct();
		}
	}
}