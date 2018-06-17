using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapClapMgr : MonoBehaviour {

    public static ClapClapMgr instance;
    public MusicData musicData;
    public PlayerData playerData;

    void Awake()
    {
        instance = this;
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
