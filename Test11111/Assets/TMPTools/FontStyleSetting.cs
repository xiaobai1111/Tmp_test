using System;
using System.Collections.Generic;
using System.IO;
using Packages.com.unity._1._6.Scripts.Runtime;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
    [InitializeOnLoad]
    public class EditorFontStyleSetting
    {
        static EditorFontStyleSetting()
        {
            if (!Application.isPlaying)
            {
                Tmp_FontUnderlayInfos.Reset();
                Tmp_FontUnderlayInfos.GetConfigTextById = delegate(int id) { return TextConfig.GetTextById(id); };
            }
            else
            {
                /// todo
            }

        }
    }
#endif
