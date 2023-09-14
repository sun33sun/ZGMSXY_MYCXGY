using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class Drilling : ViewController
	{
		void Start()
		{
			Bit_Big.Play("Lopping");
			Drill.Play("Learn_Wood");
		}
	}
}
