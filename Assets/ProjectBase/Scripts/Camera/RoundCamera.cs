using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ProjectBase
{
    public class FollowPersonView : IPersonView
    {
        public Transform transform { get; set; }
        public PersonViewField PvField { get; set; }
        CinemachineVirtualCamera cam;

        public FollowPersonView(CinemachineVirtualCamera cam,PersonViewField pvf)
        {
            this.cam = cam;
            transform = this.cam.transform;
            this.PvField = pvf;
        }
        
        public void UpdateMovement(Vector2 dir)
        {
        }

        public void OnMouseSliding(Vector2 slidingValue)
        {
            Vector3 euler = cam.transform.transform.localEulerAngles;
            cam.transform.rotation = Quaternion.AngleAxis(-slidingValue.y, cam.transform.right) *
                                     Quaternion.AngleAxis(slidingValue.x, cam.transform.up) * cam.transform.rotation;
            euler = cam.transform.transform.localEulerAngles;
            if (euler.z != 0)
            {
                euler.z = 0;
                cam.transform.transform.localEulerAngles = euler;
            }
        }

        public void OnQState()
        {
        }

        public void OnEState()
        {
        }

        public void OnMouseScrollWheel(float distance)
        {
            cam.m_Lens.FieldOfView -= distance * PvField.viewSpeed;
            if (cam.m_Lens.FieldOfView < 1)
                cam.m_Lens.FieldOfView = 1;
            else if (cam.m_Lens.FieldOfView > 90)
                cam.m_Lens.FieldOfView = 90;
        }

        public void Reset()
        {
        }
    }
}