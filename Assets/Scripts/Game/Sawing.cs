using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class Sawing : ViewController
	{
		void Start()
		{
			TableSaw.Play("Lopping");
			Wood.Play("Learn_TableSaw");
		}
	}
}
