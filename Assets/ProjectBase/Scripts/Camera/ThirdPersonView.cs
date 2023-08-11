using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase
{
	public class ThirdPersonView : IPersonView
	{
		Transform m_transform;
		public Transform transform
		{
			get => m_transform;
			set
			{
				m_transform = value;
				cam = m_transform.GetComponent<CinemachineVirtualCamera>();
				target = cam.Follow;
			}
		}
		PersonViewField pvField;
		public PersonViewField PvField { get => pvField; set { pvField = value; } }
		CinemachineVirtualCamera cam;
		Transform target;

		public ThirdPersonView(Transform transform)
		{
			this.transform = transform;
			pvField.moveSpeed = 3;
			pvField.upSpeed = 2;
			pvField.rotateSpeed = 3;
			pvField.viewSpeed = 10;
		}

		public void UpdateVelocity(Vector2 dir)
		{
			//m_transform.rotation = Quaternion.Lerp(Quaternion.identity, m_transform.rotation,Time.deltaTime);
		}

		public void Reset()
		{
			pvField.moveSpeed = 3;
			pvField.upSpeed = 2;
			pvField.rotateSpeed = 3;
			pvField.viewSpeed = 10;
		}

		public void OnMouseSliding(Vector2 slidingValue)
		{
			m_transform.rotation = Quaternion.AngleAxis(-slidingValue.y, m_transform.right) * Quaternion.AngleAxis(slidingValue.x, m_transform.up) * m_transform.rotation;
			Vector3 euler = m_transform.transform.localEulerAngles;
			if (euler.z != 0)
			{
				euler.z = 0;
				m_transform.transform.localEulerAngles = euler;
			}
		}

		public void OnEState()
		{
		}

		public void OnQState()
		{
		}

		public void OnMouseScrollWheel(float distance)
		{
			cam.m_Lens.FieldOfView += distance * pvField.viewSpeed;
			if (cam.m_Lens.FieldOfView < 1)
				cam.m_Lens.FieldOfView = 1;
			else if (cam.m_Lens.FieldOfView > 90)
				cam.m_Lens.FieldOfView = 90;
		}
	}
}
