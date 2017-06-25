using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour {

	//InitializationInformation
	public float shootFromDistance =0.0f;
	public Bullet bulletPrefab;

	//Timing
	public float unloadSpeed = 0.0f;
	private float lastShot = 0.0f;

    public GameObject MuzzleTip;




	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Fire()
	{

		if ((Time.time - lastShot) > unloadSpeed)
		{
			lastShot = Time.time;
			GameObject spawnedBullet = Instantiate (bulletPrefab.gameObject, MuzzleTip.gameObject.transform.position + MuzzleTip.transform.forward * shootFromDistance, MuzzleTip.transform.rotation);
			Bullet b = spawnedBullet.GetComponent<Bullet> ();
			b.SetDirection (MuzzleTip.transform.forward);
			b.SetSource (this.gameObject);


			Animator a = this.gameObject.transform.root.GetComponentInChildren<Animator> ();
			if (a != null)
			{
				a.SetTrigger ("Shoot");
			}
			// Raycast to find the tile infront of the player
			RaycastHit hit;

            Debug.DrawRay(MuzzleTip.transform.position, -transform.up, Color.white, 3.0f);
			if (Physics.Raycast(MuzzleTip.transform.position, -transform.up, out hit, 100.0f))
			{
				Tile frontTile = hit.collider.gameObject.GetComponent<Tile>();

				if(frontTile == null)
				{
					return;
				}
				else
				{
					frontTile.onLock(transform.GetComponent<Player>());
				}


			}
		}    
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward);
	}
}
