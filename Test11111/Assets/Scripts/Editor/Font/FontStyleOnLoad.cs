using System;
using Packages.com.unity._1._6.Scripts.Runtime;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FontStyleOnLoad : AssetPostprocessor
{ 
    static FontStyleOnLoad()
    { 
        
    }
    
            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                foreach (var importedAsset in importedAssets)
                {
                    if (importedAsset == FontStyleSetting.configPath)
                    {
                               Debug.LogError("导入初始化222");
                        EditorApplication.update += EditorUpdate; 
                        return;
                    }
                }
            }
    
    private static void EditorUpdate()
    {
            Tmp_FontUnderlayInfos.Ins.Clear();
            Tmp_FontUnderlayInfos.initFontUnderlayInfoDelegate(Tmp_FontUnderlayInfos.Ins, false);
            
            TMPro_EventManager.FONTSTYLEID_CHANGED_EVENT.Call(null);
            
            EditorApplication.update -= EditorUpdate;
    }
}