﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ExecuteFind : Editor
{
    [MenuItem("GameObject/查找物体", false, 49)]
    public static void TempExecute()
    {
        //清空消息窗口
        var logEntries = typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries");
        var clearMethod = logEntries.GetMethod("Clear",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);

        //获取所有子物体
        GameObject parent = UnityEditor.Selection.gameObjects[0];
        //获取Hierarchy窗口的所有物体
        // List<Transform> childs = new List<Transform>();
        // GetChildrens(parent.transform, childs);
        
        //获取所有子物体
        // Transform[] childs = Resources.FindObjectsOfTypeAll<Transform>();


        List<Transform> childs = new List<Transform>();
        GetChildrens(parent.transform, childs);
        for (int i = 0; i < childs.Count; i++)
        {
            Transform child = childs[i];
            if (child.CompareTag("RoundPoint"))
            {
                EditorGUIUtility.PingObject(child);
                Debug.Log(child.name);
            }
        }
    }

    static void GetChildrens(Transform parent, List<Transform> childs)
    {
        childs.Add(parent);

        if (parent.childCount != 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GetChildrens(parent.GetChild(i), childs);
            }
        }
    }
}