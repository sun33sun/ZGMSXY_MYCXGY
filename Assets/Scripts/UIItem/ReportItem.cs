using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZGMSXY_MYCXGY
{
    public struct ReportData
    {
        public string reportName;
        public DateTime startTime;
        public DateTime endTime;
        public int score;
    }

    public class ReportItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpName;
        [SerializeField] private TextMeshProUGUI tmpStartTime;
        [SerializeField] private TextMeshProUGUI tmpEndTime;
        [SerializeField] private TextMeshProUGUI tmpTotalTime;
        [SerializeField] private TextMeshProUGUI tmpScore;

        public void LoadData(ReportData data)
        {
            tmpName.text = data.reportName;
            tmpStartTime.text = data.startTime.ToString("MM-dd HH:mm");
            tmpEndTime.text = data.endTime.ToString("MM-dd HH:mm");
            tmpTotalTime.text = (data.endTime - data.startTime).ToString("m'm's's'");
            tmpScore.text = data.score.ToString();
        }
    }
}