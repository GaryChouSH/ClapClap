using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class AudioController : MonoBehaviour {
	public static AudioController instance;
	[HideInInspector]
	public const float MAX_DECIBEL = 0f;
	[HideInInspector]
	public const float MIN_DECIBEL = -80f;

	private const string Music = "Music";
	private const string Sound = "Sound";

	public AudioMixer audioMixer;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		audioMixer.SetFloat (Music, (PlayerPrefs.GetInt (Music) > 0) ? MAX_DECIBEL : MIN_DECIBEL);
		audioMixer.SetFloat (Sound, (PlayerPrefs.GetInt (Sound) > 0) ? MAX_DECIBEL : MIN_DECIBEL);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setFloat(string item, float value){
		audioMixer.SetFloat (item, value);
	}


}

