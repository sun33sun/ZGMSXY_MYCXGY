using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  ProjectBase
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected Animator _animator;
        internal Transform _Camera;
        protected Vector3 newForward;
        [SerializeField] protected int speed = 10;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        internal virtual void UpdateMovement(Vector2 dir)
        {
            Vector3 addPos = (dir.y * transform.forward + dir.x * transform.right) * Time.deltaTime * speed;
            _rb.MovePosition(addPos + transform.localPosition);
            if (_Camera != null && (dir.x != 0 || dir.y != 0))
            {
                newForward = _Camera.forward;
                newForward.y = 0;
                transform.forward = newForward;
            }
        }
    }
}