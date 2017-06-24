using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour {

    public Material m_Material;

	void Start () {
		foreach(Transform child in transform)
        {
            child.GetComponent<Renderer>().material = m_Material;
        }
	}
	
	void Update () {
		
	}
}
