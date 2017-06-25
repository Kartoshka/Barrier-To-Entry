using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Assets;
using Cinemachine;

public class RotateAround : MonoBehaviour {

	public GameObject target;
	public float speed =10;
	public CinemachineVirtualCamera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround (target.transform.position, target.transform.up, speed * Time.deltaTime);
		if (cam != null)
		{
			cam.TransposerTrackingOffset = this.transform.position;
		}
	}
}
