using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace Packages.com.unity._1._6.Scripts.Runtime
{
    public class Tmp_FontUnderlayInfos
    {
        public delegate string GetConfigTextByIdDelegate(int id);
        public static GetConfigTextByIdDelegate GetConfigTextById;
        
        
        public static void Reset()
        {
            // todo
        }
    }

}