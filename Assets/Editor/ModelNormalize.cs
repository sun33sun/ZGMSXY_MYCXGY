using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ModelNormalize : Editor
{
    [MenuItem("GameObject/执行代码", false, 49)]
    public static void NormalizeAll()
    {
        GameObject root = Selection.activeGameObject;
    }
}
