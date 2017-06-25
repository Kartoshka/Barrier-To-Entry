using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

	public Text roundTimer;
	public Text roundStartTimer;

	public RectTransform p1LifeZone;
	public RectTransform p2LifeZone;
	
	// Use this for initialization
	void Start () {
		RoundManager.OnRoundStart += OnRoundStart;

		if (roundStartTimer != null)
		{
			roundStartTimer.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (roundTimer != null)
		{
			roundTimer.text = RoundManager.instance.GetRoundTimeLeft ().ToString ("F");
		}
		//Update HP
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
