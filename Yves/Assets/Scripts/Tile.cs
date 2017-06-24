using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Level m_Level;
    public STATE m_State;
    public List<Tile> m_NeighborList;

    public enum STATE
    {
        NONE,
        LOCKED
    }

	void Start () {
        
        // Get level instance
        m_Level = transform.parent.GetComponent<Level>();


	}
	
	void Update () {
		
	}

    public void onLock()
    {
        m_State = STATE.LOCKED;

        transform.GetComponent<Renderer>().material = m_Level.m_HitMaterial;
    }
}
