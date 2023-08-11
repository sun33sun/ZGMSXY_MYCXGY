using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase.Anim
{
	public class AnimMgr : Singleton<AnimMgr>
	{
		public async UniTaskVoid Play(Animation anim, string clipName)
		{
			anim.Play(clipName);
			await UniTask.WaitUntil(() => !anim.isPlaying);
		}

		public async UniTask Play(Animator anim, string clipName)
		{
			anim.Play(clipName);
			await UniTask.Yield(PlayerLoopTiming.Update);
			await UniTask.WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
		}
	}
}
