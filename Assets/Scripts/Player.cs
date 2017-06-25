using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	public delegate void DeathAction (string deadPlayer);
	public static event DeathAction OnPlayerDeath;

    public Material m_PlayerMaterial;
    public Material m_OtherPlayerMaterial;

    public GameObject m_Blocker;
    public int m_PlayerNumber;

	// Use this for initialization
	void Start () {
		
        foreach(Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer)
                renderer.material = m_PlayerMaterial;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
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

}
