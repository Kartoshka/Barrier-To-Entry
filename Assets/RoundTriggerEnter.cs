using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTriggerEnter : MonoBehaviour {


	public string triggerScript;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == triggerScript)
		{
			RoundManager.instance.RequestNewRound ();
		}
	}

}
