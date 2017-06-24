using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public float shootFromDistance =0.0f;
	public float unloadSpeed = 0.0f;
	private float lastShot = 0.0f;
	float bulletSpeed;

    public GameObject MuzzleTip;

	private Vector3 lastPosition;
	private Vector3 moveVelocity;
	public Bullet bulletPrefab;




	// Use this for initialization
	void Start () {
		lastPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		moveVelocity = (this.transform.position - lastPosition)/Time.deltaTime;
		lastPosition = this.transform.position;
		Debug.Log (moveVelocity);
	}


	public void Fire()
	{

		if ((Time.time - lastShot) > unloadSpeed)
		{
			lastShot = Time.time;
			GameObject spawnedBullet = Instantiate (bulletPrefab.gameObject, this.gameObject.transform.position + this.transform.forward * shootFromDistance, this.transform.rotation);
			Bullet b = spawnedBullet.GetComponent<Bullet> ();
			b.SetValues (this.transform.forward, moveVelocity);

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
