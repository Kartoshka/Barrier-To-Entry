﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float birthTime;
	public float duration;
	public float speed;

	private Vector3 direction;

	// Use this for initialization
	void Start () {
		birthTime = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birthTime) < duration)
		{
			this.transform.position += speed * direction * Time.deltaTime;
			this.transform.rotation = Quaternion.LookRotation (direction, new Vector3 (0, 0, 1));
		} else
		{
			Destroy (this.gameObject);
		}
	}

	public void SetValues(Vector3 dir)
	{
		direction = dir;
	}
}
