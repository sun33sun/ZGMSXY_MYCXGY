using System;
using Cysharp.Threading.Tasks;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ProjectBase;
using UnityEngine;
using QAssetBundle;
using ProjectBase.Anim;

namespace ZGMSXY_MYCXGY
{
    public class Main : PersistentMonoSingleton<Main>
    {
        private IEnumerator Start()
        {
            // 加载UI
            // yield return UIKit.PreLoadPanelAsync<MaskPanel>(UILevel.PopUI,prefabName:Settings.UI + MaskPanel.Name);
            yield return UIKit.OpenPanelAsync<TopPanel>(UILevel.PopUI, prefabName: Settings.UI + TopPanel.Name);
            yield return ExtensionFunction.PreLoadPanelAsync<MainPanel>(UILevel.Bg, prefabName: Settings.UI + MainPanel.Name);
            yield return ExtensionFunction.PreLoadPanelAsync<KnowledgePanel>(prefabName: Settings.UI + KnowledgePanel.Name);
            UIKit.GetPanel<KnowledgePanel>().HideSync();
            yield return ExtensionFunction.PreLoadPanelAsync<LearnPanel>(prefabName: Settings.UI + LearnPanel.Name);
            UIKit.GetPanel<LearnPanel>().HideSync();
            yield return ExtensionFunction.PreLoadPanelAsync<InteractionPanel>(prefabName: Settings.UI + InteractionPanel.Name);
            UIKit.GetPanel<InteractionPanel>().HideSync();
            yield return ExtensionFunction.PreLoadPanelAsync<EvaluatePanel>(prefabName: Settings.UI + EvaluatePanel.Name);
            UIKit.GetPanel<EvaluatePanel>().HideSync();
            yield return ExtensionFunction.PreLoadPanelAsync<ReportPanel>(prefabName: Settings.UI + ReportPanel.Name);
            UIKit.HidePanel<ReportPanel>();
            UIKit.GetPanel<ReportPanel>().HideSync();
            yield return ExtensionFunction.PreLoadPanelAsync<PreviewPanel>(prefabName: Settings.UI + PreviewPanel.Name);
            UIKit.GetPanel<PreviewPanel>().HideSync();
            // UIKit.HidePanel<MaskPanel>();
        }
    }
}