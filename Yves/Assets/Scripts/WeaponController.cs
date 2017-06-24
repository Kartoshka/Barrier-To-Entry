using System.Collections;
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

        // Raycast to find the tile infront of the player
        RaycastHit hit;

        Vector3 direction = Quaternion.Euler(0, -45, 0) * transform.forward;
        Debug.DrawRay(transform.position, direction, Color.blue, 3.0f);
        if (Physics.Raycast(transform.position, direction, out hit, 100.0f))
        {
            Tile frontTile = hit.collider.gameObject.GetComponent<Tile>();

            if(frontTile == null)
            {
                Debug.Log("Couldnt't find front tile!");
                return;
            }
            else
            {
                frontTile.onLock();
            }


        }
            
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (this.transform.position, this.transform.position + this.transform.forward);
	}
}
