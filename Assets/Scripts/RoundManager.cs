using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    public GameObject Player1Logic;
    public GameObject Player2Logic;

	void Start () {
        StartCoroutine(StartRound());
	}
	
	void Update () {
		
	}

    IEnumerator StartRound()
    {
        Player1Logic.SetActive(false);
        Player2Logic.SetActive(false);
        yield return new WaitForSeconds(3.0f);

        Player1Logic.SetActive(true);
        Player2Logic.SetActive(true);
    }
}
