using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Level m_Level;
    public STATE m_State;
    public List<Tile> m_NeighborList;

    public GameObject m_DynamicMeshRed;
    public GameObject m_DynamicMeshBlue;

    public enum STATE
    {
        NONE,
        LOCKED
    }

	void Start () {
        
        // Get level instance
        m_Level = transform.parent.GetComponent<Level>();
        m_DynamicMeshBlue = m_Level.m_DynamicMeshBlue;
        m_DynamicMeshRed = m_Level.m_DynamicMeshRed;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
            m_NeighborList.Add(hit.transform.GetComponent<Tile>());
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 100.0f))
            m_NeighborList.Add(hit.transform.GetComponent<Tile>());
        if (Physics.Raycast(transform.position, transform.right, out hit, 100.0f))
            m_NeighborList.Add(hit.transform.GetComponent<Tile>());
        if (Physics.Raycast(transform.position, -transform.right, out hit, 100.0f))
            m_NeighborList.Add(hit.transform.GetComponent<Tile>());

		
	}
    void Update()
    {
    }

    public void onLock(Player player)
    {
        if (m_State != STATE.LOCKED && m_Level.m_DebugEnableTileLock)
        {
            string playerTag = player.tag;

            Collider collider = null;

            if (player.m_PlayerNumber == 1)
            {
                GameObject dynR = Instantiate(m_DynamicMeshRed, transform);
                collider = dynR.GetComponent<Collider>();
                m_State = STATE.LOCKED;
            }
            else if (player.m_PlayerNumber == 2)
            {
                GameObject dynB = Instantiate(m_DynamicMeshBlue, transform);
                collider = dynB.GetComponent<Collider>();
                m_State = STATE.LOCKED;
            }
            else
            {
                Debug.Log("INVALID PLAYER CASE IN ONLOCK!");
            }

            if(collider)
            {
                collider.enabled = false;
                StartCoroutine(LateColliderEnable(collider));
            }
        }

    }

    IEnumerator LateColliderEnable(Collider collider)
    {
        yield return new WaitForSeconds(1f);

        collider.enabled = true;
    }
}
