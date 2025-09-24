using System.Collections.Generic;
public class TextConfig
{
    public static Dictionary<int, string> UITextMap = new Dictionary<int, string>();
    public static string GetTextById(int id)
    {
        string text = id.ToString() + "(Test)";
        //  这里注释，去实现自己的逻辑
        /*if (!UITextMap.TryGetValue(id, out text))
        {

            var luaState = LuaManager.lua;
            if (null != luaState)
            {
                text = luaState.Invoke<int, string>("TextForID", id, false);
                    
                UITextMap.Add(id, text);
            }
            else
            {
                using (LuaTable item = UIMisc.GetRecordItem("syslang_syslang_cn", id))
                {
                    if (null == item)
                    {
                        text = "invalid id " + id;
                        return text;
                    }
                    text = item["cn"] as string;
                }
            }
        }*/
        return text;
    }
}
