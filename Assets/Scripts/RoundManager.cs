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

	public int m_p1_lives;
	public int m_p2_lives;

	public GameObject m_active_p1;
	public GameObject m_active_p2;

	public Transform p1_spawn_point;
	public Transform p2_spawn_point;

	private bool m_roundStarted = false;
	private bool m_gameEnded = false;


	//Level spawning
	int level =0;
	public float distanceBetweenLevels = 100.0f;
	public Vector3 directionSpawnWorld;
	public float delay;
	public GameObject levelPrefab;
	private GameObject m_loadedLevel;

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

		m_p1_lives = m_p2_lives = player_lives;
	}

	void Start () {
		Player.OnPlayerDeath += OnPlayerDeath;
		RestartRound ();
	}
		
	void RestartRound()
	{
		SpawnNewWorld ();
		DisableCurrentPlayers ();
		StartCoroutine(StartRound());
		m_timeLeft = lengthofRound;
	}


	void SpawnNewWorld()
	{

		GameObject temp = Instantiate (levelPrefab, directionSpawnWorld * distanceBetweenLevels * level++, Quaternion.identity);
        BaseLevel baseLevel = temp.GetComponent<BaseLevel>();

		if (m_loadedLevel != null)
		{
			GameObject oldLevel = m_loadedLevel;
			Destroy (oldLevel, delay);
		}
		m_loadedLevel = temp;

		p1_spawn_point = baseLevel.m_P1Spawn.transform;
		p2_spawn_point = baseLevel.m_P2Spawn.transform;

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


	private IEnumerator EndRound()
	{
		m_roundStarted = false;
		//Play Effect

		yield return new WaitForSeconds (3.0f);
		DisableCurrentPlayers ();
		yield return new WaitForSeconds (1.0f);
		RestartRound ();

		//Display winner
		//Wait for input?

		yield return null;
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

	public void OnPlayerDeath(string tag)
	{
		if (tag == m_active_p1.tag)
		{
			m_p1_lives--;
		} else if (tag == m_active_p2.tag)
		{
			m_p2_lives--;
		}

		if (!CheckEndGameCondition ())
		{
			StartCoroutine(EndRound ());
		} else
		{
			//End Game

			m_roundStarted = false;
			m_gameEnded = true;
		}

	}

	private bool CheckEndGameCondition()
	{
		if (m_p1_lives <= 0)
		{
			//OnGameWin (m_active_p2);
		} else if (m_p2_lives <= 0)
		{
			//OnGameWin (m_active_p1);
		} else
		{
			return false;
		}
		return true;

	}
}
