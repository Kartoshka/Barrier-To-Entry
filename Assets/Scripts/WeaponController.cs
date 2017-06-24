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
			GameObject spawnedBullet = Instantiate (bulletPrefab.gameObject, this.gameObject.transform.position + this.transform.forward * shootFromDistance, this.transform.rotation);
			Bullet b = spawnedBullet.GetComponent<Bullet> ();
			b.SetDirection (this.transform.forward);
			b.SetSource (this.gameObject);

			// Raycast to find the tile infront of the player
			RaycastHit hit;

			Vector3 direction = Quaternion.Euler(35, 0, 0) * transform.forward;
			Debug.DrawRay(transform.position, direction, Color.blue, 3.0f);
			if (Physics.Raycast(MuzzleTip.transform.position, -transform.up, out hit, 100.0f))
			{
				Tile frontTile = hit.collider.gameObject.GetComponent<Tile>();

				if(frontTile == null)
				{
					Debug.Log("Couldnt't find front tile!");
					return;
				}
				else
				{
					frontTile.onLock(transform.GetComponent<Player>().m_OtherPlayerMaterial);
				}


			}
		}    
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward);
	}
}
