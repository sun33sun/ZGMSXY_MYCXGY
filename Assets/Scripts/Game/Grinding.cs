using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class Grinding : ViewController
	{
		void Start()
		{
			Sander.Play("Lopping");
			Wood.Play("Learn_Sander");
		}
	}
}
