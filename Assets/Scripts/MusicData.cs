using System.Collections.Generic;
using System;
using MiniJSON;

[Serializable] //可序列化
public class MusicData {

    public int nowPlayMusicID;
    private Dictionary<int, MusicInfoStruct> musicInfoDict;
    private List<int> musicList;

    public MusicData(string jsonStr)
    {
        musicInfoDict = new Dictionary<int, MusicInfoStruct>();
        musicList = new List<int>();
    }

    private MusicInfoStruct getMusicDataByID(int musicID)
    {
        MusicInfoStruct info = new MusicInfoStruct();
        info.ID = musicID;
        info.Name = "Pegasus Fantasy";
        info.BPM = 166;
        info.Lyrics = "unknow";
        info.Composer = "unknow";
        return info;
    }

    public MusicInfoStruct getMainGameMusicInfo()
    {
        return getMusicDataByID(nowPlayMusicID);
    }

}

public struct MusicInfoStruct
{
    public long ID;
    public string Name;
    public int BPM;
    public string Lyrics;
    public string Composer;
}
