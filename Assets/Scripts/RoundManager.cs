using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {


	public delegate void GameWin(GameObject victor);
	public static event GameWin OnGameWin;

	public float m_waitTime;
	public float beginingOfRoundDelay = 3.0f;
	public float m_startTime;
	private float m_timeLeft; 
	public float lengthofRound = 30.0f;

    public GameObject p1_prefab;
    public GameObject p2_prefab;

	[Range(1,5)]
	public int player_lives;

	private int m_p1_lives;
	private int m_p2_lives;

	private GameObject m_active_p1;
	private GameObject m_active_p2;

	public Transform p1_spawn_point;
	public Transform p2_spawn_point;

	private bool m_roundStarted = false;
	private bool m_gameEnded = false;

	public static RoundManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

		//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(this.gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		Player.OnPlayerDeath += OnPlayerDeath;
	}

	void Start () {
		RestartRound ();
	}
		
	void RestartRound()
	{
		DisableCurrentPlayers ();
		StartCoroutine(StartRound());
		m_timeLeft = lengthofRound;
	}

	void DisableCurrentPlayers()
	{
		if (m_active_p1!=null)
		{
			Destroy (m_active_p1);
//			PlayerInputManager p1 = m_active_p1.GetComponentInChildren<PlayerInputManager> ();
//			if (p1 != null)
//			{
//				p1.enabled = false;
//			}
//			m_active_p1 = null;
		}

		if (m_active_p2!=null)
		{
			Destroy (m_active_p2);
//			PlayerInputManager p2 = m_active_p2.GetComponentInChildren<PlayerInputManager> ();
//			if (p2 != null)
//			{
//				p2.enabled = false;
//			}
//			m_active_p2 = null;
		}
	}

	void Update () {
		if (m_roundStarted)
		{
			m_timeLeft -= Time.deltaTime;
			if (m_timeLeft < 0)
			{
				m_timeLeft = 0;
				m_roundStarted = false;
			}
		}
	}


	private void EndRound()
	{
		m_roundStarted = false;
		DisableCurrentPlayers ();
		RestartRound ();

		//Display winner
		//Wait for input?
	}

    IEnumerator StartRound()
    {
       
		m_waitTime = beginingOfRoundDelay;

		while (m_waitTime > 0)
		{
			yield return new WaitForSeconds(0.1f);
			m_waitTime -= 0.1f;
		}

		m_active_p1 = Instantiate (p1_prefab, p1_spawn_point.position, p1_spawn_point.rotation);
		m_active_p2 = Instantiate (p2_prefab, p2_spawn_point.position, p2_spawn_point.rotation);

		m_roundStarted = true;
		m_startTime = Time.time;
    }

	public void OnPlayerDeath(GameObject player)
	{
		if (player == m_active_p1)
		{
			m_p1_lives--;
		} else if (player == m_active_p2)
		{
			m_p2_lives--;
		}

		if (!CheckEndGameCondition ())
		{
			EndRound ();
		}

	}

	private bool CheckEndGameCondition()
	{
		if (m_p1_lives <= 0)
		{
			OnGameWin (m_active_p2);

		} else if (m_p2_lives <= 0)
		{
			OnGameWin (m_active_p1);
		} else
		{
			return false;
		}

		m_roundStarted = false;
		m_gameEnded = true;
		return true;
	}
}
