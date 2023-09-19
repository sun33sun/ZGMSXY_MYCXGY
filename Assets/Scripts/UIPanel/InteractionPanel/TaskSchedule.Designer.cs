/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class TaskSchedule
	{
		[SerializeField] public RectTransform TaskContent;

		public void Clear()
		{
			TaskContent = null;
		}

		public override string ComponentName
		{
			get { return "TaskSchedule";}
		}
	}
}
