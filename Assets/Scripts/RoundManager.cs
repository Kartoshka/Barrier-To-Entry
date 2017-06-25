using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour {


	public delegate void WinEvent(GameObject victor);
	public delegate void RoundEvent();
	public static event WinEvent OnGameWin;
	public static event WinEvent OnRoundWin;
	public static event RoundEvent OnRoundEnd;
	public static event RoundEvent OnRoundStart;

	public float m_waitTime;
	public float beginingOfRoundDelay = 3.0f;
	public float m_startTime;
	private float m_timeLeft; 
	public float lengthofRound = 30.0f;
	public float roundEndWait = 2.0f;

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
		//StartCoroutine(StartRound());
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
		//	Destroy (m_active_p1);
			PlayerInputManager p1 = m_active_p1.GetComponentInChildren<PlayerInputManager> ();
			if (p1 != null)
			{
				p1.enabled = false;
			}		
		}

		if (m_active_p2!=null)
		{
//			Destroy (m_active_p2);
			PlayerInputManager p2 = m_active_p2.GetComponentInChildren<PlayerInputManager> ();
			if (p2 != null)
			{
				p2.enabled = false;
			}
		}
		
	}

	void Update () {
		if (m_roundStarted)
		{
			m_timeLeft -= Time.deltaTime;
			if (m_timeLeft < 0)
			{
				m_timeLeft = 0;
				m_p1_lives--;
				m_p2_lives--;
				DisableCurrentPlayers ();

				if (!CheckEndGameCondition ())
				{
					StartCoroutine(EndRound());;
				}
			}
		}

		if (Input.GetButtonDown ("restart"))
		{
			SceneManager.LoadScene (0);
		}
	}


	private IEnumerator EndRound()
	{
		m_roundStarted = false;

		if (OnRoundEnd != null)
		{
			OnRoundEnd ();
		}
		//Play Effect

		yield return new WaitForSeconds (roundEndWait);
		DisableCurrentPlayers ();
		RestartRound ();

		//Display winner
		//Wait for input?
		yield return null;
	}

	public void RequestNewRound()
	{
		if (m_roundStarted)
		{
			StartCoroutine(EndRound());;
		}

		m_waitTime = beginingOfRoundDelay;
		StartCoroutine (StartRound ());
		OnRoundStart ();


	}
    IEnumerator StartRound()
    {
		if (!CheckEndGameCondition ())
		{
			m_waitTime = beginingOfRoundDelay;

			while (m_waitTime > 0)
			{
				yield return new WaitForSeconds(0.1f);
				m_waitTime -= 0.1f;
			}

			m_active_p1 = Instantiate (p1_prefab, p1_spawn_point.position, p1_spawn_point.rotation);
			m_active_p2 = Instantiate (p2_prefab, p2_spawn_point.position, p2_spawn_point.rotation);

			m_timeLeft = lengthofRound;
			m_roundStarted = true;
			m_startTime = Time.time;
		}
    }

	public void OnPlayerDeath(string tag)
	{
		bool p1Win = false;
		bool p2Win = false;
		if (tag == m_active_p1.tag)
		{
			p2Win = true;
			m_p1_lives--;
		} else if (tag == m_active_p2.tag)
		{
			p1Win = true;

			m_p2_lives--;
		}

		if (!CheckEndGameCondition ())
		{
			if (p1Win)
			{
				if (OnRoundWin != null)
				{
					OnRoundWin (m_active_p1);
				}
			} else if (p2Win)
			{
				if (OnRoundWin != null)
				{
					OnRoundWin (m_active_p2);
				}
			}
			StartCoroutine(EndRound());
		} else
		{
			//End Game
			m_roundStarted = false;
			m_gameEnded = true;
		}

	}

	private bool CheckEndGameCondition()
	{
		bool p1Win =m_p1_lives <= 0;
		bool p2Win =m_p2_lives <= 0 ;

		if (p1Win)
		{
			if (p2Win)
			{
				m_roundStarted = false;
				if (OnGameWin != null)
				{
					OnGameWin (this.gameObject);
				}
			} else
			{
				m_roundStarted = false;
				if (OnGameWin != null)
				{
					OnGameWin (m_active_p1);
				}
			}
		} else if (p2Win)
		{
			m_roundStarted = false;
			if (OnGameWin != null)
			{
				OnGameWin (m_active_p2);
			}
		} 
		return p1Win || p2Win;

	}


	public float GetRoundTimeLeft()
	{
		return m_timeLeft;
	}
	public float GetRoundStartTimer()
	{
		return m_waitTime;
	}

	public int GetHealth(int playerNum){
		if (playerNum == 1)
		{
			return m_p1_lives;
		} else if (playerNum == 2)
		{
			return m_p2_lives;
		} else
		{
			return -1;
		}
	}
}
