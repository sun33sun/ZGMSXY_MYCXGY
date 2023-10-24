using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
    public class ReportPanelData : UIPanelData
    {
    }

    public partial class ReportPanel : UIPanel
    {
        [SerializeField] private List<ReportItem> reportItems;
        List<ReportData> _datas = new List<ReportData>();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as ReportPanelData ?? new ReportPanelData();
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            btnBack.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
            {
                await this.HideAsync();
                await UIKit.GetPanel<MainPanel>().ShowAsync();
            }, token));
            btnSubmit.onClick.AddListener(Settings.GetButtonIgnoreClickFunc(btnBack, async () =>
            {
                await this.HideAsync();
                await UIKit.GetPanel<MainPanel>().ShowAsync();
            }, token));
            tmpDate.text = DateTime.Now.ToString("yyyy-MM-dd");
            InitReportData();
        }

        void InitReportData()
        {
            foreach (var reportItem in reportItems)
            {
                ReportData data = new ReportData()
                {
                    reportName = reportItem.name,
                    startTime = DateTime.Now,
                    endTime = DateTime.Now,
                    score = 0
                };
                _datas.Add(data);
            }
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            UIKit.GetPanel<TopPanel>().tmpTip.text = "在该页面查看实验报告";
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        public void LoadReport(ReportData data)
        {
            int totalScore = 0;
            for (var i = 0; i < reportItems.Count; i++)
            {
                if (reportItems[i].name.Equals(data.reportName))
                {
                    ReportData oldData = _datas[i];
                    reportItems[i].LoadData(data);
                    _datas[i] = data;
                }

                totalScore += _datas[i].score;
            }
            tmpTotalScore.text = totalScore.ToString();
        }
    }
}