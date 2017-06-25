using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource m_Background;
    public AudioSource m_Ding;

	// Use this for initialization
	void Start () {
        m_Background.Play();

    }

    public void playDing()
    {
        m_Ding.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
