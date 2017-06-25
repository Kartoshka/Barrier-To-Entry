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
        //StartCoroutine(SpawnWall(player));

        string playerTag = player.tag;

        if(player.m_PlayerNumber == 1)
        {
            Instantiate(m_DynamicMeshRed, transform);
        }
        else if(player.m_PlayerNumber == 2)
        {
            Instantiate(m_DynamicMeshBlue, transform);
        }
        else
        {
            Debug.Log("INVALID PLAYER CASE IN ONLOCK!");
        }
    }

    IEnumerator SpawnWall(Player player)
    {
        yield return new WaitForSeconds(0.1f);

        m_State = STATE.LOCKED;
        transform.GetComponent<Renderer>().material = player.m_OtherPlayerMaterial;
        Instantiate(player.m_Blocker, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity, transform);
    }
}
