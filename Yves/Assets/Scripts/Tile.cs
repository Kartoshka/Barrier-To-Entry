using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Level m_Level;
    private STATE m_State;

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

    public STATE GetState() { return m_State; }

    public void onLock()
    {
        m_State = STATE.LOCKED;

        transform.GetComponent<Renderer>().material = m_Level.m_HitMaterial;
    }
}
