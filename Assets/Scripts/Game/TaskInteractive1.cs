using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ZGMSXY_MYCXGY
{
    public partial class TaskInteractive1 : ViewController
    {
        private GameLibrary _library;
        InteractionPanel _panel;
        private ToolSelections _selections;
        private CancellationToken _token;

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

        //1		用尺子在材料上画线
        //1.1	画正视图
        //1.2	标注轮子的中心点
        //1.3	把结构线延长到顶面
        //1.4	画出顶视图
        //1.5	在顶部标注出需要裁切掉的部分
        //1.6	对应在底部标注出底面和另一侧的轮子位置

        //2		开始打孔（钻削Drilling）
        //2.1	使用细头钻，在左侧和右侧轮子处打孔（打通）
        //2.2	使用粗头钻，在左侧和右侧轮子处扩张打孔（不打通）

        //3		开始铣削（Milling）
        //3.1   在左侧面、右侧面的中间处，使用
        

        //4		打磨车身（磨削Grinding）
        //4.1	各侧均要打磨
        //4.2	用粗锉刀进行细部修整
        //4.3	用细锉刀进行进一步光滑处理
        //4.5	用砂纸打磨

        //5		制作轮子与轴
        //5.1	将轴插入轮子，然后将轮子多余的部分切割掉
        //5.2	打磨轮子的面和边缘
        //5.3	将轮子与轴装到车身
        //5.4	重复上述操作制作后轮并安装到车身
        public void StartTask(GameLibrary library, InteractionPanel panel, ToolSelections selections)
        {
            _library = library;
            _panel = panel;
            _selections = selections;
            DrawLine().Forget();
        }

        async UniTaskVoid DrawLine()
        {
            await _selections.WaitButtonsClick(ToolSelections.ToolType.Ruler, ToolSelections.ToolType.Pencil);
            Animator animatorDrawLine = Instantiate(Task1_DrawLine, transform);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrawLine, "Measure2"));
            await _panel.WaitNext();
            Destroy(animatorDrawLine.gameObject);
            Drilling().Forget();
        }

        async UniTaskVoid Drilling()
        {
            await _selections.WaitButtonsClick(ToolSelections.ToolType.DrillAndMillingMachine,
                ToolSelections.ToolType.Bit_Fine, ToolSelections.ToolType.Bit_Coarse);
            Animator animatorDrilling = Instantiate(Task1_Drilling, transform);
            await UniTask.WaitUntil(GetAnimEndFunc(animatorDrilling, "Task1_Drilling3"));
            await _panel.WaitNext();
            // Destroy(animatorDrilling.gameObject);
        }

        async UniTaskVoid Milling()
        {
            // await _selections.WaitButtonsClick(ToolSelections.ToolType.DrillAndMillingMachine,);
            
        }
    }
}