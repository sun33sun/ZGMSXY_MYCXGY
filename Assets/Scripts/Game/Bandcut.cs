using UnityEngine;
using QFramework;

namespace ZGMSXY_MYCXGY
{
	public partial class Bandcut : ViewController
	{
		void Start()
		{
			Wood.Play("Learn_BandSaw");
			BandSaw.Play("Lopping");
		}
	}
}
