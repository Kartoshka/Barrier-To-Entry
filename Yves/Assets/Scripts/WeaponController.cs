﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	float shootFromDistance =0.0f;
	float bulletSpeed;

	public Bullet bulletPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Fire()
	{
		GameObject spawnedBullet = Instantiate (bulletPrefab.gameObject, this.gameObject.transform.position + this.transform.forward * shootFromDistance, this.transform.rotation);
		Bullet b = spawnedBullet.GetComponent<Bullet> ();
		b.SetValues (this.transform.forward);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward);
	}
}
