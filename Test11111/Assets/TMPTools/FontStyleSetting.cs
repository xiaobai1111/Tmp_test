using System;
using System.Collections.Generic;
using System.IO;
using Packages.com.unity._1._6.Scripts.Runtime;
using Sirenix.OdinInspector;
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
                        Tmp_FontUnderlayInfos.initFontUnderlayInfoDelegate =
                                delegate(Tmp_FontUnderlayInfos infos, bool forceApplayTexture)
                                {
                                        infos.Clear();
                                        FontStyleSetting fontStyleSetting = AssetDatabase.LoadAssetAtPath<FontStyleSetting>(FontStyleSetting.configPath);
                                        fontStyleSetting.ReLoadDatas(forceApplayTexture);
                                        FontStyleSetting.InitTmp_FontUnderlayInfos(infos, fontStyleSetting);
                                };
                        Tmp_FontUnderlayInfos.GetConfigTextById = delegate(int id)
                        {
                                return TextConfig.GetTextById(id);
                        };
                }
                else
                {
                        /// todo
                }
        }

        public static void InitTmp_FontUnderlayInfos(Tmp_FontUnderlayInfos tmp_FontUnderlayInfos,
                FontStyleSetting fontStyleSetting)
        {
                tmp_FontUnderlayInfos.FontColorInfos = fontStyleSetting.FontColorInfos;
                tmp_FontUnderlayInfos.FontSizeToUnderlayDilateInfos = fontStyleSetting.DilateInfos;
                tmp_FontUnderlayInfos.FontUnderlayColorInfos = fontStyleSetting.FontUnderlayColorInfos;
        }
}



[CreateAssetMenu(fileName = "FontStyleSetting", menuName = "ScriptableObjects/FontStyleSetting", order = 0)]
    public class FontStyleSetting : ScriptableObject
    {
        public static readonly string configPath = "Assets/AssetBundles/TTFs/FontStyleSetting.asset";
#if UNITY_EDITOR
        [EnableIf("@1!=1")]
#endif
        public string FontColorInfosMD5;
#if UNITY_EDITOR
        [TableList]
#endif
        public List<TMP_FontColorInfo> FontColorInfos;
        
#if UNITY_EDITOR
        [EnableIf("@1!=1")]
#endif
        public string DilateInfosMD5;
#if UNITY_EDITOR
        [TableList]
#endif
        public List<TMP_FontSizeToUnderlayDilateInfo> DilateInfos;
#if UNITY_EDITOR
        [EnableIf("@1!=1")]
#endif
        public string FontUnderlayColorInfosMD5;
#if UNITY_EDITOR
        [TableList]
#endif
        public List<TMP_FontUnderlayColorInfo> FontUnderlayColorInfos;
        
        
        public static void InitTmp_FontUnderlayInfos(Tmp_FontUnderlayInfos tmp_FontUnderlayInfos, FontStyleSetting fontStyleSeeting)
        {
                if (tmp_FontUnderlayInfos == null)
                {
                        Debug.LogError("tmp_FontUnderlayInfos is null");
                        return;
                }
                if(fontStyleSeeting == null)
                {
                        Debug.LogError("fontStyleSeeting is null");
                        return;
                }

                tmp_FontUnderlayInfos.FontColorInfos = fontStyleSeeting.FontColorInfos;
                tmp_FontUnderlayInfos.FontSizeToUnderlayDilateInfos = fontStyleSeeting.DilateInfos;
                tmp_FontUnderlayInfos.FontUnderlayColorInfos = fontStyleSeeting.FontUnderlayColorInfos;
        }
        
        public void ReLoadDatas(bool forceApplayTexture)
        {
                string fontColorPath = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName,
                        "Assets/data/fontcolor_fontcolor.txt");
                string underlayColorPath = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName,
                        "Assets/data/fontcolor_underlaycolor.txt");
                string fontSizeToUnderlayDilatePath = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName,
                        "Assets/data/fontcolor_fontsizetounderlaydilate.txt");
                
                string fontColorPathMD5 = ABFileUtils.MD5File(fontColorPath);
                string underlayColorPathMD5 = ABFileUtils.MD5File(underlayColorPath);
                string fontSizeToUnderlayDilatePathMD5 = ABFileUtils.MD5File(fontSizeToUnderlayDilatePath);
                if (
                        fontColorPathMD5 == this.FontColorInfosMD5 &&
                        underlayColorPathMD5 == this.FontUnderlayColorInfosMD5 &&
                        fontSizeToUnderlayDilatePathMD5 == this.DilateInfosMD5

                )
                {
                        return;
                }
                
                #region 字体颜色
                string[] colorInfos = File.ReadAllLines(fontColorPath);
                this.FontColorInfos = new List<TMP_FontColorInfo>();
                for (var i = 0; i < colorInfos.Length; i++)
                {
                        string colorInfoStr = colorInfos[i].Trim();
                        TMP_FontColorInfo fontColorInfo = ConvertColorInfo(colorInfoStr);
                        this.FontColorInfos.Add(fontColorInfo);
                }
                #endregion
                
                #region 设置描边数据
                this.FontUnderlayColorInfos = new List<TMP_FontUnderlayColorInfo>();
           
                string[] underlayColorStrInfos = File.ReadAllLines(underlayColorPath);
            
                for (var i = 0; i < underlayColorStrInfos.Length; i++)
                {
                        string underlayColorStrInfo = underlayColorStrInfos[i];
                        string[] info = underlayColorStrInfo.Split('\t');
               
                        Color color = new Color();
                        if (ConverColor(1, info, out color))
                        {
                                TMP_FontUnderlayColorInfo fontUnderlayColorInfo = new TMP_FontUnderlayColorInfo();
                                fontUnderlayColorInfo.id = Convert.ToInt32(info[0]);
                                fontUnderlayColorInfo.color = color;
                                this.FontUnderlayColorInfos.Add(fontUnderlayColorInfo);
                        }
                }
           
                if (forceApplayTexture || this.FontUnderlayColorInfosMD5 != underlayColorPathMD5)
                {
                        this.FontUnderlayColorInfosMD5 = underlayColorPathMD5;
              
                        this.ApplyTexture();
                }
           
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                #endregion
                
                #region 字体大小对应的描边大小
                string[] fontSizeToUnderlayDilateStrs = File.ReadAllLines(fontSizeToUnderlayDilatePath);
                this.DilateInfos = new List<TMP_FontSizeToUnderlayDilateInfo>();
                for (var i = 0; i < fontSizeToUnderlayDilateStrs.Length; i++)
                {
                        string colorInfoStr = fontSizeToUnderlayDilateStrs[i].Trim();
                        string[] info = colorInfoStr.Split('\t');
                        TMP_FontSizeToUnderlayDilateInfo fontSizeToUnderlayDilateInfo =
                                new TMP_FontSizeToUnderlayDilateInfo();
                        fontSizeToUnderlayDilateInfo.FontSize = Convert.ToInt32(info[0]);
                        fontSizeToUnderlayDilateInfo.Dilates = new List<float>();
                        for (int j = 1; j < info.Length; j++)
                        {
                    
                                fontSizeToUnderlayDilateInfo.Dilates.Add(Convert.ToSingle(info[j]));    
                        }
                        this.DilateInfos.Add(fontSizeToUnderlayDilateInfo);
                }
                #endregion
        }
        
        public TMP_FontColorInfo ConvertColorInfo(string colorInfoStr)
        {
            string[] info = colorInfoStr.Split('\t');
            TMP_FontColorInfo fontColorInfo = new TMP_FontColorInfo();
            fontColorInfo.id = Convert.ToInt32(info[0]);
            Color color = new Color();
            if (ConverColor(1, info, out color))
            {
                fontColorInfo.color = color;
            }
            Color color2 = new Color();
            fontColorInfo.isTopColor = ConverColor(2, info, out color2);
            if(fontColorInfo.isTopColor)
            {
                fontColorInfo.topColor = color2;
            }
            Color color3 = new Color();
            fontColorInfo.isBottomColor = ConverColor(3, info, out color3);
            if(fontColorInfo.isBottomColor)
            {
                fontColorInfo.bottomColor = color3;
            }
            return fontColorInfo;
        }

        public bool ConverColor(int index,string[] info,out Color color)
        {
            color = new Color();
            if (info.Length <= index)
            {
                color.r = 1;
                color.g = 1;
                color.b = 1;
            }
            else
            {
                string colorStr = info[index];
                if (!string.IsNullOrEmpty(colorStr))
                {
                    
                    string[] rgb = colorStr.Split(',');
                    float r = (float)Mathf.Min(Convert.ToInt32(rgb[0]), byte.MaxValue)/byte.MaxValue;
                    float g = (float)Mathf.Min(Convert.ToInt32(rgb[1]), byte.MaxValue)/byte.MaxValue;
                    float b = (float)Mathf.Min(Convert.ToInt32(rgb[2]), byte.MaxValue)/byte.MaxValue;
                    if (rgb.Length >= 4)
                    {
                        float a = (float)Mathf.Min(Convert.ToInt32(rgb[3]), byte.MaxValue)/byte.MaxValue;
                        color = new Color(r,g,b,a);
                    }
                    else
                    {
                        color = new Color(r,g,b);
                    }
                   
                    return true;
                }
            }

            return false;
        }
        
#if UNITY_EDITOR
        [Button("重新加载配置表")]
#endif
        public void ReLoadDatas()
        {
                ReLoadDatas(false);
                Tmp_FontUnderlayInfos.ResetIns();
        }
        
#if UNITY_EDITOR
        [Button("设置材质")]
#endif
        public void ApplyTexture()
        {
            
                Texture2D colorTxt =new Texture2D(100, 100);
                int h = 0;
                int v = 0;   
                for (var i = 0; i < FontUnderlayColorInfos.Count; i++)
                {
                
                        TMP_FontUnderlayColorInfo fontColorInfo = FontUnderlayColorInfos[i];
                        int index = fontColorInfo.id - 1;
                        int x = index % 100;
                        int y = (int)Math.Floor((float) index / 100.0);
               
                        colorTxt.SetPixel(x, y, fontColorInfo.color); 
                }
                colorTxt.Apply();
                var bytes = colorTxt.EncodeToPNG();
                File.WriteAllBytes("Assets/AssetBundles/TTFs/UnderlayColor.png",bytes);
                AssetDatabase.Refresh();
        }
    }

#endif
