using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;

public class AudioPool : MonoBehaviour
{
    //output
    private AudioMixerGroup SoundEffectOutput;
    //BGM source
    public Music BGM;
    //sound effect source
    public SoundEffect _clips;
    //BGM player
    public AudioSource Background;
    //SoundEffect Container
    private Transform SoundEffectPool;

    void Awake()
    {
        AudioSet();
    }

    private void AudioSet()
    {
        SoundEffectPool = this.transform;
        SoundEffectOutput = AudioController.instance.audioMixer.FindMatchingGroups("Sound")[0];
    }

    public void StopAudio(string objName)
    {
        foreach (Transform ByObj in SoundEffectPool)
        {
            if (ByObj.name == objName)
            {
                Destroy(ByObj.gameObject);
            }
        }
    }

    public bool hasAudio(string objName)
    {
        foreach (Transform ByObj in SoundEffectPool)
        {
            if (ByObj.name == objName)
            {
                return true;
            }
        }
        return false;
    }


    public delegate void AddSound(ClipSet _cloneSound, string _soundName = null);
    public AddSound _AddSound;
    public void DoSound(ClipSet _clone, string _cloneName)
    {
        if (_clone._clip == null) return;
        GameObject NewAudio = new GameObject();
        AudioSource _cloneSource = NewAudio.AddComponent<AudioSource>();
        NewAudio.transform.SetParent(SoundEffectPool, false);
        NewAudio.transform.name = _cloneName == null ? _clone._clip.name : _cloneName;
        NewAudio.SetActive(NewAudio);

        _cloneSource.outputAudioMixerGroup = SoundEffectOutput;
        _cloneSource.clip = _clone._clip;
        _cloneSource.mute = _clone.Mute;
        _cloneSource.playOnAwake = _clone.PlayOnAwake;
        _cloneSource.loop = _clone.Loop;
        _cloneSource.priority = _clone.Priority;
        _cloneSource.volume = _clone.Volume;
        _cloneSource.pitch = _clone.Pitch;
        _cloneSource.Play();
        if (!_clone.Loop && NewAudio != null)
        {
            _Clear = DoClear;
            _Clear.Invoke(NewAudio, _cloneSource.clip.length);
        }
    }

    public delegate void ClearInvoke(GameObject _clone, float WaitTime = 0);
    public ClearInvoke _Clear;
    public void DoClear(GameObject _clone, float WaitTime)
    {
        StartCoroutine(CloneClear(_clone, WaitTime));
    }
    IEnumerator CloneClear(GameObject _clone, float WaitTime)
    {
        if (_clone != null) yield return new WaitForSeconds(WaitTime);
        if (_clone != null) _clone.SetActive(false);
        if (_clone != null) Destroy(_clone);
    }

    //打擊音效
    public void Btn_Spin()
    {
        _AddSound = DoSound;
        _AddSound.Invoke(_clips.Click, "Click");
    }
    //自訂義要的音效
    public void CustomizeSound(ClipSet cilp, string name)
    {
        _AddSound = DoSound;
        _AddSound.Invoke(cilp, name);
    }

    // --------------------------------------------- BGM
    public void BGMPause(bool isPause = false)
    {
        if (isPause)
        {
            Background.Pause();
        }
        else
        {
            Background.UnPause();
        }
    }

    public void DoBGM(AudioClip _AudioClip)
    {
        if (Background.clip == _AudioClip)
            return;
        Background.clip = _AudioClip;
        Background.Pause();
        Background.volume = 0;
        Background.Play();
        Background.volume = .2f;
        StopCoroutine("BGM_Coroutine");
        StartCoroutine("BGM_Coroutine");
    }
    IEnumerator BGM_Coroutine()
    {
        float BGMax = BGM.BGMax;

        float i = Background.volume;
        while (i <= BGMax)
        {
            i += .01f;
            Background.volume = i;
            yield return new WaitForSeconds(.01f);
        }
        Background.volume = BGMax;
    }

    //載入歌
    public void PlayMusic(string songName)
    {
        AudioClip song = Resources.Load(songName) as AudioClip;
        BGM.MainGameBGM = song;
        DoBGM(BGM.MainGameBGM);
    }

    public void PlayBGM()
    {
        DoBGM(BGM.BGM);
    }
}

[Serializable]
public class Music
{
    [Range(0.0f, 1.0f)]
    public float BGMax = .65f;
    //遊戲音樂
    public AudioClip MainGameBGM;
    public AudioClip BGM;
}

[Serializable]
public class SoundEffect
{
    //打擊音效
    public ClipSet Click;

}

[Serializable]
public class ClipSet
{
    public AudioClip _clip;
    // public bool DestroyItSelf = true;
    public bool Mute = false;
    public bool PlayOnAwake = false;
    public bool Loop = false;
    [Range(0, 1)]
    public int Priority = 0;
    [Range(0.0f, 1.0f)]
    public float Volume = .6f;
    [Range(0.0f, 1.0f)]
    public float Pitch = 1f;
}