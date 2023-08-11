using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;

namespace ZGMSXY_MYCXGY
{
	public class ModelRoot : SingletonMono<ModelRoot>
	{
		public Camera modelC;
		public Light dirLight;
		public GameObject cube;

		protected override void Awake()
		{
			base.Awake();
			gameObject.SetActive(false);
		}
	}
}

