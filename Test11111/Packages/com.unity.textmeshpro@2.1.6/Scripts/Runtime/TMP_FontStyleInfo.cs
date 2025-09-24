using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace Packages.com.unity._1._6.Scripts.Runtime
{
        [Serializable]
        public class TMP_FontUnderlayColorInfo
        {
                /*/// <summary>
                /// 投影 大小
                /// </summary>
                public float underlayDilate;*/
                
                /// <summary>
                /// 对应颜色索引
                /// </summary>
                public Color color;

                public int id;
        }
        
        [Serializable]
        public class TMP_FontSizeToUnderlayDilateInfo
        {
                public int FontSize;
                public List<float> Dilates;
        }
        
    [Serializable]
    public class TMP_FontColorInfo
    {
        public int id;
        public Color color;
#if UNITY_EDITOR
        [HideIf("@1==1")]
#endif
        public bool isTopColor;
#if UNITY_EDITOR
        [ShowIf("isTopColor")]
#endif
        public Color topColor;
#if UNITY_EDITOR
        [HideIf("@1==1")]
#endif
        public bool isBottomColor;
#if UNITY_EDITOR
        [ShowIf("isBottomColor")]
#endif
        public Color bottomColor;
    }
    
    public class Tmp_FontUnderlayInfos
    {
        public delegate string GetConfigTextByIdDelegate(int id);
        public delegate void InitFontUnderlayInfoDelegate(Tmp_FontUnderlayInfos fontUnderlayInfos, bool forceApplayTexture);
        public static InitFontUnderlayInfoDelegate initFontUnderlayInfoDelegate;
        public static GetConfigTextByIdDelegate GetConfigTextById;
        
        #region FontColorInfos
        private Dictionary<int, TMP_FontColorInfo> fontColorInfoMap { get; set; }
        private List<TMP_FontColorInfo> m_FontColorInfos;
        public List<TMP_FontColorInfo> FontColorInfos
        { 
            set
            {
                    m_FontColorInfos = value;
                    if (fontColorInfoMap == null)
                            fontColorInfoMap = new Dictionary<int, TMP_FontColorInfo>();
                    else
                            fontColorInfoMap.Clear();
                    foreach (var info in m_FontColorInfos)
                    {
                            fontColorInfoMap.Add(info.id, info);
                    }
            }
            get
            {
                    return m_FontColorInfos;
            }
        }

        #endregion

        #region FontSizeToUnderlayDilateInfos

        private Dictionary<float, TMP_FontUnderlayColorInfo> m_FontUnderlayColorInfoMap;
        private List<TMP_FontUnderlayColorInfo> m_FontUnderlayColorInfo;
        public List<TMP_FontUnderlayColorInfo> FontUnderlayColorInfos
        {
                get
                {
                        return m_FontUnderlayColorInfo;
                }
                set
                {
                        m_FontUnderlayColorInfo = value;
                        if (m_FontUnderlayColorInfoMap == null)
                                m_FontUnderlayColorInfoMap = new Dictionary<float, TMP_FontUnderlayColorInfo>();
                        else
                                m_FontUnderlayColorInfoMap.Clear();
                        foreach (var tmpFontUnderlayColorInfoInfo in m_FontUnderlayColorInfo)
                        {
                                m_FontUnderlayColorInfoMap.Add(tmpFontUnderlayColorInfoInfo.id, tmpFontUnderlayColorInfoInfo);   
                        }
                }
        }
        
        #endregion
        
        #region FontSizeToUnderlayDilateInfos

        private  Dictionary<float, TMP_FontSizeToUnderlayDilateInfo> m_fontSizeToUnderlayDilateInfoMap;

        private List<TMP_FontSizeToUnderlayDilateInfo> m_fontSizeToUnderlayDilateInfos;
        public List<TMP_FontSizeToUnderlayDilateInfo> FontSizeToUnderlayDilateInfos
        {
                get {return m_fontSizeToUnderlayDilateInfos; }
                set
                {
                        m_fontSizeToUnderlayDilateInfos = value;
                        m_fontSizeToUnderlayDilateInfoMap = new Dictionary<float, TMP_FontSizeToUnderlayDilateInfo>();
                        foreach (var tmpFontSizeToUnderlayDilateInfo in m_fontSizeToUnderlayDilateInfos)
                        {
                                m_fontSizeToUnderlayDilateInfoMap.Add(tmpFontSizeToUnderlayDilateInfo.FontSize,tmpFontSizeToUnderlayDilateInfo);   
                        }
                }
        }
        #endregion
        
        private static Tmp_FontUnderlayInfos _ins;
        public static Tmp_FontUnderlayInfos Ins
        {
                get
                {
                        if (_ins == null)
                        {
                                _ins = new Tmp_FontUnderlayInfos();
                        }
#if UNITY_EDITOR
                        if (_ins.FontColorInfos == null)
                        {
                                _ins = new Tmp_FontUnderlayInfos();
                        }       
#endif
                        return _ins;
                }
        }
        private Dictionary<Texture,float> m_textureToUnderlayDilate;
        public Tmp_FontUnderlayInfos()
        { 
            if (initFontUnderlayInfoDelegate != null)
            {
                try
                {
                    initFontUnderlayInfoDelegate(this, false);
                }
                catch (Exception e)
                {
                   Debug.LogError("字体样式初始化失败:"+e.Message + "\n" + e.StackTrace);
                }
               
            }
            m_textureToUnderlayDilate = new Dictionary<Texture, float>();
        }
        
        public static void ResetIns()
        {
                _ins = null;
            
        }
        public static void Reset()
        {
                _ins = null;
        }
        
        

        public void Clear()
        {
                m_FontColorInfos?.Clear();
                m_fontSizeToUnderlayDilateInfos?.Clear();
                fontColorInfoMap?.Clear();
                m_fontSizeToUnderlayDilateInfos?.Clear();
                m_FontUnderlayColorInfo?.Clear();
                m_FontUnderlayColorInfoMap?.Clear(); 
        }

        /// <summary>
        /// 根据id 获得颜色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TMP_FontColorInfo GetFontColorInfo(int id)
        {
                TMP_FontColorInfo fontColorInfo = null;
                fontColorInfoMap.TryGetValue(id, out fontColorInfo);
                return fontColorInfo;
        }
    }
}