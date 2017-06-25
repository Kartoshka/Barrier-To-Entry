using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource m_Background;
    public AudioSource m_Ding;
    public AudioSource m_ReadyUp;

	// Use this for initialization
	void Start () {
        m_Background.Play();

        DontDestroyOnLoad(this);
    }

    public void playDing()
    {
        m_Ding.Play();
    }

    public void playReady()
    {
        m_ReadyUp.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
