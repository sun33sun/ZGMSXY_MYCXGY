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
        // Ray _ray = new Ray(transform.position, this.transform.forward);
        // Physics.Raycast(_ray, out _hit, 10);
        
        Debug.DrawRay(transform.position, transform.forward * length, drawColor);
        
        // Debug.Log(_hit.point);
    }
}