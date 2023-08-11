using Cysharp.Threading.Tasks;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QAssetBundle;
using ProjectBase.Anim;

namespace ZGMSXY_MYCXGY
{
	public class Main : MonoBehaviour
	{
		private IEnumerator Start()
		{
			yield return UIKit.PreLoadPanelAsync<TopPanel>(UILevel.PopUI, prefabName: Settings.UI + TopPanel.Name);
			yield return UIKit.PreLoadPanelAsync<MainPanel>(UILevel.Bg, prefabName: Settings.UI + MainPanel.Name);
			yield return UIKit.PreLoadPanelAsync<KnowledgePanel>(UILevel.Common, prefabName: Settings.UI + KnowledgePanel.Name);
			UIKit.HidePanel<KnowledgePanel>();
			yield return UIKit.PreLoadPanelAsync<LearnPanel>(UILevel.Common, prefabName: Settings.UI + LearnPanel.Name);
			UIKit.HidePanel<LearnPanel>();
		}


	}
}

