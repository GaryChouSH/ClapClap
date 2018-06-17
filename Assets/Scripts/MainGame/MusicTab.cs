using MiniJSON;
using System.Collections.Generic;
using System;

[Serializable]
public class MusicTab
{
    public List<object> tabList;

    public MusicTab(string jsonStr)
    {
        tabList = new List<object>();
        tabList = Json.Deserialize(jsonStr) as List<object>;
    }



}
