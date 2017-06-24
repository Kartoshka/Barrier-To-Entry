using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Level m_Level;
    private STATE m_State;

    enum STATE
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

    STATE GetState() { return m_State; }

    void onLock()
    {
        m_State = STATE.LOCKED;
    }
}
