using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

	public Text roundTimer;
	public Text roundStartTimer;

	public RectTransform p1LifeZone;
	public RectTransform p2LifeZone;
	public float offset;

    public GameObject m_SoundManager;

	public Image healthPrefab;
	public Image p1HealthPrefab;
	public Image p2HealthPrefab;
	private List<GameObject> p1Health;
	private List<GameObject> p2Health;
	
	// Use this for initialization
	void Start () {
		RoundManager.OnRoundStart += OnRoundStart;
		//Player.OnPlayerDeath += OnPlayerDeath;
		if (roundStartTimer != null)
		{
			roundStartTimer.gameObject.SetActive (false);
		}
		int player1Health = RoundManager.instance.GetHealth (1);

		int player2Health = RoundManager.instance.GetHealth (2);

        m_SoundManager = GameObject.FindGameObjectWithTag("Sound");

        //generateHealth (player1Health, player2Health);
    }


	void generateHealth(int p1HP, int p2HP){
		int maxLives = RoundManager.instance.player_lives;

		if (p1Health!=null)
		{
			foreach (GameObject g in p1Health)
			{
				Destroy (g);
			}
		}

		if (p2Health!=null)
		{
			foreach (GameObject g in p2Health)
			{
				Destroy (g);
			}
		}

		if (healthPrefab == null)
		{
			return;
		}

		p1Health = new List<GameObject> ();
		p2Health = new List<GameObject> ();

		for (int i = 0; i < p1HP; i++)
		{
			GameObject p1 = Instantiate (p1HealthPrefab.gameObject);
			RectTransform t1 = p1.GetComponent<RectTransform> ();
			p1.transform.parent = p1LifeZone.transform;
			t1.anchoredPosition = p1LifeZone.anchoredPosition;

			t1.anchoredPosition += new Vector2 (i * offset, 0);

			p1Health.Add (p1);
		}
		for (int i = 0; i < p2HP; i++)
		{
			GameObject p2 = Instantiate (p2HealthPrefab.gameObject);
			RectTransform t2 = p2.GetComponent<RectTransform> ();
			p2.transform.parent = p2LifeZone.transform;
			t2.anchoredPosition = p2LifeZone.anchoredPosition;

			t2.anchoredPosition -= new Vector2 (i * offset, 0);

			p2Health.Add (p2);
		}
	}
	// Update is called once per frame
	void Update () {

		int player1Health = RoundManager.instance.GetHealth (1);

		int player2Health = RoundManager.instance.GetHealth (2);
		if (p1Health == null || p2Health == null || player1Health != p1Health.Count || player2Health != p2Health.Count)
		{
			generateHealth (player1Health, player2Health);
		}

		if (roundTimer != null)
		{
			roundTimer.text = RoundManager.instance.GetRoundTimeLeft ().ToString ("F");
		}

		//Update HP
	}


	void OnPlayerDeath(){
		int player1Health = RoundManager.instance.GetHealth (1);

		int player2Health = RoundManager.instance.GetHealth (2);
		if (player1Health != p1Health.Count || player2Health != p2Health.Count)
		{
			generateHealth (player1Health, player2Health);
		}
	}
	void OnRoundStart()
	{
		if (roundStartTimer != null)
		{
			roundStartTimer.gameObject.SetActive (true);
			StartCoroutine (updateRoundTimer ());
		}
	}

	void OnRoundEnd()
	{
		if (roundTimer != null)
		{
			roundTimer.gameObject.SetActive (false);
		}
	}

	private IEnumerator updateRoundTimer()
	{
		float roundTime;
		int secondTime;
		do
		{
			roundTime = RoundManager.instance.GetRoundStartTimer ();
			secondTime = Mathf.CeilToInt(roundTime);
			roundStartTimer.text = ""+secondTime;

            m_SoundManager = GameObject.FindGameObjectWithTag("Sound");
            SoundManager soundManager = m_SoundManager.GetComponent<SoundManager>();
            soundManager.playDing();
			yield return new WaitForEndOfFrame();
		} while(secondTime > 0 );


		if (roundTimer != null)
		{
			roundTimer.gameObject.SetActive (true);
		}

		roundStartTimer.text = "START";

		roundStartTimer.gameObject.AddComponent<FadeTextOverTime> ().fadeDuration = 3.0f;

		yield return null;
		
	}
}
