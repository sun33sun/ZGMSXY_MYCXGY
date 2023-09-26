using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectBase
{
    public class ModelCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private bool _isEnable = false;
        [SerializeField] Transform _target;
        private PersonViewField _personViewField;
        private bool isRotate = false;

        private void Start()
        {
            InputMgr.GetInstance().ChangerInput(true);
            EventCenter.GetInstance().AddEventListener<float>("鼠标滚轮", OnMouseScrollWheel);
            EventCenter.GetInstance().AddEventListener<Vector2>("鼠标滑动", OnMouseSliding);
            EventCenter.GetInstance().AddEventListener("鼠标右键按下", OnMouseRightDown);
            EventCenter.GetInstance().AddEventListener("鼠标右键抬起", OnMouseRightUp);

            _personViewField.moveSpeed = 3;
            _personViewField.upSpeed = 2;
            _personViewField.rotateSpeed = 3;
            _personViewField.viewSpeed = 10;
        }

        public void StartLook(Transform target)
        {
            _target = target;
            _isEnable = true;
        }

        private void OnMouseRightDown()
        {
            if (!_isEnable)
                return;
            isRotate = true;
        }

        private void OnMouseRightUp()
        {
            if (!_isEnable)
                return;
            isRotate = false;
        }

        private void OnMouseSliding(Vector2 slidingValue)
        {
            if (!isRotate)
                return;
            Transform camTransform = _camera.transform;
            Vector3 euler = camTransform.localEulerAngles;
            camTransform.rotation *= Quaternion.AngleAxis(-slidingValue.y, camTransform.right) *
                                     Quaternion.AngleAxis(slidingValue.x, camTransform.up);
            euler = camTransform.localEulerAngles;
            if (euler.z != 0)
            {
                euler.z = 0;
                camTransform.localEulerAngles = euler;
            }

            print($"slidingValue.x : {slidingValue.x}\t\tslidingValue.y : {slidingValue.y}");
        }

        private void OnMouseScrollWheel(float distance)
        {
            if (!_isEnable)
                return;
            _camera.m_Lens.FieldOfView -= distance * _personViewField.viewSpeed;
            if (_camera.m_Lens.FieldOfView < 1)
                _camera.m_Lens.FieldOfView = 1;
            else if (_camera.m_Lens.FieldOfView > 90)
                _camera.m_Lens.FieldOfView = 90;
        }
    }
}