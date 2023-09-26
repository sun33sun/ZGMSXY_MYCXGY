using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using QFramework;
using ProjectBase;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ProjectBase
{
    public class CameraManager : SingletonMono<CameraManager>
    {
        List<IPersonView> personViews = new List<IPersonView>();
        PersonViewField pvField;
        [SerializeField] bool isEnable = false;
        public PersonViewType pvType = PersonViewType.FirstPerson;
        public RenderTexture rt;

        public CinemachineVirtualCamera nowC
        {
            get
            {
                switch (pvType)
                {
                    case PersonViewType.None:
                        return noneC;
                    case PersonViewType.FirstPerson:
                        return firstC;
                    case PersonViewType.ThirdPerson:
                        return thirdC;
                    case PersonViewType.FollowObject:
                        return followC;
                    case PersonViewType.RoundObject:
                        return roundC;
                    default:
                        return noneC;
                }
            }
        }

        [SerializeField] Camera mainC;
        [SerializeField] CinemachineVirtualCamera noneC = null;
        [SerializeField] CinemachineVirtualCamera firstC = null;
        [SerializeField] CinemachineVirtualCamera followC = null;
        [SerializeField] CinemachineVirtualCamera thirdC = null;
        [SerializeField] CinemachineVirtualCamera roundC = null;

        //漫游相机的刚体
        Rigidbody roamRig = null;

        Vector3 originPos;
        Vector3 originAngle;
        float originFieldOfView;
        Vector3 nowPos;

        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                if (value)
                    roamRig.constraints = RigidbodyConstraints.FreezeRotation;
                else
                    roamRig.constraints = RigidbodyConstraints.FreezeAll;
                isEnable = value;
            }
        }

        bool isRotate = false;

        private void Start()
        {
            //查找组件
            roamRig = firstC.GetComponent<Rigidbody>();
            //记录初始位置
            originPos = firstC.transform.position;
            originAngle = firstC.transform.rotation.eulerAngles;
            originFieldOfView = firstC.m_Lens.FieldOfView;

            #region 移动与旋转

            //移动
            InputMgr.GetInstance().ChangerInput(true);
            //上下移动
            EventCenter.GetInstance().AddEventListener(KeyCode.LeftControl + "保持", OnEState);
            EventCenter.GetInstance().AddEventListener<float>("鼠标滚轮", OnMouseScrollWheel);
            //旋转
            EventCenter.GetInstance().AddEventListener("鼠标右键按下", OnMouseRightDown);
            EventCenter.GetInstance().AddEventListener("鼠标右键抬起", OnMouseRightUp);
            EventCenter.GetInstance().AddEventListener<Vector2>("鼠标滑动", OnMouseSliding);
            EventCenter.GetInstance().AddEventListener(KeyCode.Space + "保持", OnQState);
            EventCenter.GetInstance().AddEventListener<Vector2>("移动方向", UpdateVelocity);

            #endregion

            #region 初始化属性

            pvField.moveSpeed = 3;
            pvField.upSpeed = 2;
            pvField.rotateSpeed = 3;
            pvField.viewSpeed = 10;
            personViews.Add(new NonePersonView());
            personViews.Add(new FirstPersonView(firstC.transform, pvField));
            personViews.Add(new ThirdPersonView(thirdC, pvField));
            personViews.Add(new RoundCamera(roundC, pvField));
            personViews.Add(new FollowPersonView(followC,pvField));
            #endregion
        }

        #region 移动、上下移动、旋转、缩放视野

        void UpdateVelocity(Vector2 dir)
        {
            personViews[(int)pvType].UpdateMovement(dir);
        }

        private void OnEState()
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnEState();
        }

        private void OnQState()
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnQState();
        }

        private void OnMouseRightDown()
        {
            if (!IsEnable)
                return;
            isRotate = true;
        }

        private void OnMouseRightUp()
        {
            if (!IsEnable)
                return;
            isRotate = false;
        }

        private void OnMouseSliding(Vector2 slidingValue)
        {
            if (!isRotate || !IsEnable)
                return;

            personViews[(int)pvType].OnMouseSliding(slidingValue);
        }

        private void OnMouseScrollWheel(float distance)
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnMouseScrollWheel(distance);
        }

        #endregion

        #region 调整人称

        public async UniTask ThirPersonView(PlayerController playerController)
        {
            pvType = PersonViewType.ThirdPerson;
            (personViews[(int)pvType] as ThirdPersonView).Player = playerController;
            thirdC.Priority = 20;
            noneC.Priority = 10;
            firstC.Priority = 10;
            followC.Priority = 10;
            roundC.Priority = 10;
            await UniTask.Delay(2000);
        }

        public async UniTask NonePersonView()
        {
            pvType = PersonViewType.None;
            noneC.Priority = 20;
            thirdC.Priority = 10;
            firstC.Priority = 10;
            followC.Priority = 10;
            roundC.Priority = 10;
            await UniTask.Delay(2000);
        }

        public void RoundPersonView(Transform target)
        {
            pvType = PersonViewType.RoundObject;
            roundC.Follow = target;
            roundC.Priority = 20;
            noneC.Priority = 10;
            firstC.Priority = 10;
            followC.Priority = 10;
        }

        public void FollowPersonView(Transform target)
        {
            pvType = PersonViewType.FollowObject;
            followC.Follow = target;
            followC.Priority = 20;
            roundC.Priority = 10;
            noneC.Priority = 10;
            firstC.Priority = 10;
        }
        

        #endregion

        private void OnDestroy()
        {
            //上下移动
            EventCenter.GetInstance().RemoveEventListener(KeyCode.LeftControl + "保持", OnEState);
            EventCenter.GetInstance().RemoveEventListener<float>("鼠标滚轮", OnMouseScrollWheel);
            //旋转
            EventCenter.GetInstance().RemoveEventListener("鼠标右键按下", OnMouseRightDown);
            EventCenter.GetInstance().RemoveEventListener("鼠标右键抬起", OnMouseRightUp);
            EventCenter.GetInstance().RemoveEventListener<Vector2>("鼠标滑动", OnMouseSliding);
            EventCenter.GetInstance().RemoveEventListener(KeyCode.Space + "保持", OnQState);
            EventCenter.GetInstance().RemoveEventListener<Vector2>("移动方向", UpdateVelocity);
        }
    }
}