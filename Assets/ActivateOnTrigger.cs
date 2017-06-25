using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour {


	public List<GameObject> toActivate;


	void OnTriggerEnter(Collider other){
		foreach (GameObject g in toActivate)
		{
			g.SetActive (true);
		}
	}
}
