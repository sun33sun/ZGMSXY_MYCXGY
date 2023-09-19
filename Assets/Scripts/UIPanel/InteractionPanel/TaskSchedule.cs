using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Net.Mime;
/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;


namespace ZGMSXY_MYCXGY
{
    public partial class TaskSchedule : UIElement
    {
        [SerializeField] Sprite spriteCompleted;
        [SerializeField] private ScheduleItem _schedulePrefab;
        private List<ScheduleItem> _items = new List<ScheduleItem>();
        public int stateIndex = 0;

        void ClearItems()
        {
            stateIndex = 0;
            for (int i = 0; i < _items.Count; i++)
                Destroy(_items[i].gameObject);
            _items.Clear();
        }

        protected override void OnBeforeDestroy()
        {
        }

        //下一个状态
        public void NextState()
        {
            if (stateIndex >= _items.Count)
            {
                print("超出状态范围");
            }
            else
            {
                _items[stateIndex].img.sprite = spriteCompleted;
                stateIndex++;
            }
        }

        public void InitTaskState(List<string> taskList)
        {
            ClearItems();
            taskList.ForEach(task =>
            {
                ScheduleItem item = Instantiate(_schedulePrefab, TaskContent);
                _items.Add(item);
                item.tmp.text = task;
            });
            LayoutRebuilder.ForceRebuildLayoutImmediate(TaskContent);
        }

        private void OnEnable()
        {
            ClearItems();
        }
    }
}