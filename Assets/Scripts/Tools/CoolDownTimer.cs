using UnityEngine;
using System.Collections.Generic;
using System;


public class CoolDownTimer:MonoBehaviour{
    public static CoolDownTimer instance;
    // 加秒數更改事件
    public delegate void ValueChange(int second, string name);
    public delegate void CoolDownEnd(string name);
    public event CoolDownEnd coolDownEnd;
    public event ValueChange onValueChange;
    //private int endTime;
    private Dictionary<string , Dictionary<string, object>> rigsterData = new Dictionary<string, Dictionary<string, object>>();

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("chkTimer", 1f, 1f);
    }

    //
    public void setCoolDownTimer(string name, int endTime, ValueChange valueChange, CoolDownEnd coolDownEnd)
    {
        Dictionary<string, object> timerDict = new Dictionary<string, object>();
        timerDict["endTime"] = endTime;
        timerDict["valueChangeFunc"] = valueChange;
        timerDict["coolDownEndFunc"] = coolDownEnd;
        rigsterData[name] = timerDict;
    }

    private void chkTimer()
    {
        if (rigsterData.Count > 0) {
            foreach (KeyValuePair<string, Dictionary<string, object>> post_arg in rigsterData)
            {
                int nowTime = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
                int endTime = (int)post_arg.Value["endTime"];
                ValueChange valueChange = (ValueChange)post_arg.Value["valueChangeFunc"];
                CoolDownEnd coolDownEnd = (CoolDownEnd)post_arg.Value["coolDownEndFunc"];
                //CoolDownEnd
                if (nowTime >= endTime)
                {
                    coolDownEnd(post_arg.Key);
                    //removeTimer(post_arg.Key);
                    return;
                }
                valueChange(endTime - nowTime, post_arg.Key);
            }
        }
    }

    public void removeTimer(string name)
    {
        if (rigsterData.ContainsKey(name)) {
            rigsterData.Remove(name);
        }
    }
}
