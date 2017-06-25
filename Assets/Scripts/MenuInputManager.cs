using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuInputManager : MonoBehaviour {

    public bool m_Player1In;
    public bool m_Player2In;

    public GameObject m_Player1;
    public GameObject m_Player2;

    public GameObject m_Player1Lights;
    public GameObject m_Player2Lights;

    public GameObject m_StartText;

    public GameObject m_SoundManager;

    private string m_Platform;

	void Start () {
        m_Player1In = false;
        m_Player2In = false;
        m_StartText.SetActive(false);
        m_Player1Lights.SetActive(false);
        m_Player2Lights.SetActive(false);

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            m_Platform = "Windows";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            m_Platform = "OSX";
        }
    }
	
	void Update () {

        // Check for inputs
        if (Input.GetButtonDown(m_Platform + "Ready1"))
            Player1Ready();
        if (Input.GetButtonDown(m_Platform + "Ready2"))
            Player2Ready();

        if (m_Player1In && m_Player2In)
        {
            m_StartText.SetActive(true);

            if (Input.GetButtonDown(m_Platform + "Start"))
                SceneManager.LoadScene(1);
        }
	}

    void Player1Ready()
    {
        m_Player1In = true;
        m_Player1Lights.SetActive(true);

        Animator a = m_Player1.GetComponent<Animator>();
        if (a != null)
        {
            a.SetTrigger("Shoot");
        }

        m_SoundManager.GetComponent<SoundManager>().playReady();
        Debug.Log("PLAYER 1 READY");
    }

    void Player2Ready()
    {
        m_Player2In = true;
        m_Player2Lights.SetActive(true);

        Animator a = m_Player2.GetComponent<Animator>();
        if (a != null)
        {
            a.SetTrigger("Shoot");
        }

        m_SoundManager.GetComponent<SoundManager>().playReady();
        Debug.Log("PLAYER 2 READY");
    }

}
