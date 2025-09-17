using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TTest : MonoBehaviour
{
// It is generally recommended to use the OnStateUpdate attribute to control the state of properties
    [OnStateUpdate("@#(exampleList).State.Expanded = $value.HasFlag(ExampleEnum.UseStringList)")]
    public ExampleEnum exampleEnum;

    public List<string> exampleList;

    [Flags]
    public enum ExampleEnum
    {
        None,
        UseStringList = 1 << 0,
    }
}
