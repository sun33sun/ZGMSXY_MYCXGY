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
        [SerializeField] Sprite spriteInProgress;
        [SerializeField] List<Image> imgStates;
        public int stateIndex = 0;

		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

        //下一个状态
        public void NextState(){
            stateIndex++;
            if (stateIndex >= imgStates.Count){
                print("超出状态范围");
            }else{
                imgStates[stateIndex].sprite = spriteCompleted;
                stateIndex++;
            }
        }
	}
}