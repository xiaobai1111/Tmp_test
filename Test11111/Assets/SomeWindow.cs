using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class SomeWindow : OdinEditorWindow
{
    [MenuItem("My Game/My Window")]
    private static void OpenWindow()
    {
        GetWindow<SomeWindow>().Show();
    }
	
    [PropertyOrder(-10)]
    [HorizontalGroup]
    [Button(ButtonSizes.Large)]
    public void SomeButton1() { }

    [HorizontalGroup]
    [Button(ButtonSizes.Large)]
    public void SomeButton2() { }

    [HorizontalGroup]
    [Button(ButtonSizes.Large)]
    public void SomeButton3() { }

    [HorizontalGroup]
    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void SomeButton4() { }

    [HorizontalGroup]
    [Button(ButtonSizes.Large), GUIColor(1, 0.5f, 0)]
    public void SomeButton5() { }

    [TableList]
    public List<SomeType> SomeTableData;
}

public class SomeType
{
    [TableColumnWidth(50)]
    public bool Toggle;

    [AssetsOnly]
    public GameObject SomePrefab;

    public string Message;

    [TableColumnWidth(160)]
    [HorizontalGroup("Actions")]
    public void Test1() { }

    [HorizontalGroup("Actions")]
    public void Test2() { }
}
