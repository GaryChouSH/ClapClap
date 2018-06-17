using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    private MusicTab musicTab;
    private int tabCount;
    private int nowTabIdx; 
    private int bpm;

	// Use this for initialization
	void Start () {
        TextAsset jsonStr = Resources.Load("Table/MusicTab") as TextAsset;
        musicTab = new MusicTab(jsonStr.text);
        tabCount = musicTab.tabList.Count;
        bpm = ClapClapMgr.instance.musicData.getMainGameMusicInfo().BPM;
        float bpmSec = 60f / bpm / 8;
        Debug.Log(bpmSec);
        InvokeRepeating("checkTemple", 0, bpmSec);
    }

    private void checkTemple()
    {
        nowTabIdx += 1;
        if (tabCount >= nowTabIdx)
        {
            List<object> tabList = (List<object>)musicTab.tabList[nowTabIdx];
            for(int idx = 0; idx < 12; idx++)
            {
               
            }
            Debug.Log(nowTabIdx);
            Debug.Log(Time.time);
        }
    }


}
