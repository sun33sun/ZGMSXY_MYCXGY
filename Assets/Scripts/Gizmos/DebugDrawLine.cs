using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DebugDrawLine : MonoBehaviour
{
    public Color drawColor = Color.red;
    public int length = 10;
    private RaycastHit _hit;
    
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * length, drawColor);
    }
}