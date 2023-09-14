using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class QuestionController
{
    //[Header("考题模块")] 
    public enum MyChoice
    {
        单选,
        多选
    }
    public enum MyChoiceIndex
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z
    }
    public MyChoice _选项类型;
    [MultilineAttribute] 
    public string _题目;
    public Dictionary<MyChoiceIndex,string> _选项 = new Dictionary<MyChoiceIndex, string>();
    public List<MyChoiceIndex> _多选正确答案 = new List<MyChoiceIndex>();
    public MyChoiceIndex _单选或判断正确答案;

    public float _分值;
    [MultilineAttribute] 
    public string _解析内容 = "";
}
