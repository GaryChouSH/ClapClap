using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainGameInfo : MonoBehaviour
{
    private Text songNameTxt;
    private Text bpm;
    private Text Lyrics;
    private Text composer;

	// Use this for initialization
	void Start ()
    {
        songNameTxt = transform.Find("MusicName/text").GetComponent<Text>();
        bpm = transform.Find("BPM/text").GetComponent<Text>();
        Lyrics = transform.Find("Lyrics/text").GetComponent<Text>();
        composer = transform.Find("Composer/text").GetComponent<Text>();
        setMainGameInfo();
    }

    private void setMainGameInfo()
    {
        MusicInfoStruct info = ClapClapMgr.instance.musicData.getMainGameMusicInfo();
        songNameTxt.text = info.Name;
        bpm.text = info.BPM.ToString();
        Lyrics.text = info.Lyrics;
        composer.text = info.Composer;
    }
}
