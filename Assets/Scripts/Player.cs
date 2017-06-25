using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour {


	public delegate void DeathAction (string deadPlayer);
	public static event DeathAction OnPlayerDeath;

    public Material m_PlayerMaterial;
    public Material m_OtherPlayerMaterial;

    public GameObject m_Blocker;
    public int m_PlayerNumber;

	public CinemachineVirtualCamera victoryCam;
	public List<GameObject> onWin;
	public CharacterMover cMover;

	// Use this for initialization
	void Start () {

		RoundManager.OnGameWin += Gamewon;
        foreach(Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer)
                renderer.material = m_PlayerMaterial;
        }

		if (onWin != null)
		{
			foreach (GameObject c in onWin)
			{
				c.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	public void BulletHit(Bullet.BulletInfo bInfo)
	{
		switch (bInfo.m_bulletType)
		{
		case Bullet.BulletType.InstaKill:
			Destroy (this.gameObject.transform.root.gameObject);
			OnPlayerDeath (this.gameObject.transform.root.tag);
			break;
		}
	}

	public void Gamewon(GameObject player){
		if (this == null)
		{
			return;
		}
		Animator an = this.transform.root.gameObject.GetComponentInChildren<Animator> ();
		CharacterController cC = this.transform.root.gameObject.GetComponentInChildren<CharacterController> ();
		CinemachineVirtualCamera cam = victoryCam;
		if (an != null)
		{
			an.SetBool ("won", true);
		}

		if (cC != null)
		{
			cC.enabled = false;
		}

		if (cam!=null)
		{
			cam.enabled = true;
			cam.gameObject.SetActive (true);
		}

		if (cMover != null)
		{
			cMover.enabled = (false);
		}

		if (onWin != null)
		{
			foreach (GameObject c in onWin)
			{
				c.SetActive (true);
			}
		}
	}

}
