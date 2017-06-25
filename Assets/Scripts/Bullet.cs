﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public enum BulletType{
		InstaKill
	}

	public struct BulletInfo
	{
		public BulletType m_bulletType;
		public GameObject source;
	}

	private float birthTime;
	public float duration;
	public float speed;

	private Vector3 m_initialVelocity;
	private Vector3 direction;

	private GameObject source;
	// Use this for initialization
	void Start () {
		birthTime = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birthTime) < duration)
		{
			this.transform.position += ((speed * direction)) * Time.deltaTime;
			this.transform.rotation = Quaternion.LookRotation (direction, new Vector3 (0, 1, 0));
		} else
		{
			Destroy (this.gameObject);
		}
	}

	public void SetDirection(Vector3 dir)
	{
		direction = dir;
	}

	public void SetSource(GameObject s)
	{
		source = s;
	}

	void OnTriggerEnter(Collider other)
	{
        string tag = other.tag;
        if (tag == source.tag)
            return;

		if (tag == "Player1" || tag == "Player2")
		{
			OnHitPlayerEffect (other.gameObject);
		}

        if(tag == source.GetComponent<Player>().m_Blocker.tag)
        {
            Destroy(this.gameObject);
        }
	}

    protected virtual void OnHitPlayerEffect(GameObject opponent)
	{
		Destroy (opponent.gameObject);
	}
}
