using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Material m_PlayerMaterial;
    public Material m_OtherPlayerMaterial;

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

}
