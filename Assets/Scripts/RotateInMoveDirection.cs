using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInMoveDirection : MonoBehaviour {

	public bool freezeX;
	public bool freezeY;
	public bool freezeZ;

	private Vector3 lastPosition;
	public float threshold;
	[Range(0.01f,1.0f)]
	public float lerpSpeed = 1.0f;
	// Use this for initialization
	void Start () {
		lastPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 currentPos = this.transform.position;

		if (freezeX)
		{
			currentPos.x = lastPosition.x;
		}
		if (freezeY)
		{
			currentPos.y = lastPosition.y;
		}
		if (freezeZ)
		{
			currentPos.z = lastPosition.z;
		}

		if (Vector3.Distance (lastPosition, currentPos) > threshold)
		{
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, Quaternion.LookRotation (currentPos - lastPosition), lerpSpeed);
			lastPosition = currentPos;
		}
	}
}
