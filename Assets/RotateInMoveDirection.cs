using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInMoveDirection : MonoBehaviour {


	private Vector3 lastPosition;
	public float threshold;
	// Use this for initialization
	void Start () {
		lastPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 currentPos = this.transform.position;
		if (Vector3.Distance (lastPosition, currentPos) > threshold)
		{
			this.transform.rotation = Quaternion.LookRotation (currentPos - lastPosition);
			lastPosition = currentPos;
		}
	}
}
