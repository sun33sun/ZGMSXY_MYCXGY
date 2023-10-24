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
        [SerializeField] public TextMeshProUGUI tmpName;
        [SerializeField] public TextMeshProUGUI tmpStartTime;
        [SerializeField] public TextMeshProUGUI tmpEndTime;
        [SerializeField] public TextMeshProUGUI tmpTotalTime;
        [SerializeField] public TextMeshProUGUI tmpScore;

        public void LoadData(ReportData data)
        {
            tmpStartTime.text = data.startTime.ToString("MM-dd HH:mm");
            tmpEndTime.text = data.endTime.ToString("MM-dd HH:mm");
            tmpTotalTime.text = (data.endTime - data.startTime).ToString("m'm's's'");
            tmpScore.text = data.score.ToString();
        }
    }
}