using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public List<Tile> m_TileList;

    public Material m_LevelMaterial;
    public Material m_HitMaterial;
    public Material m_PathMaterial;

    public GameObject m_PilonPrefab;
    public GameObject m_DynamicMeshRed;
    public GameObject m_DynamicMeshBlue;

    public bool m_DebugEnableTileLock = true;

	// Use this for initialization
	void Start () {
        LoadTileList();
	}
	
	// Update is called once per frame
	void Update () {
        //CreatePath(5);
	}

    void LoadTileList()
    {
        // Instantiate new TileList
        m_TileList = new List<Tile>(transform.childCount);

        // Loop and add all tiles
        foreach (Transform child in transform)
        {
            m_TileList.Add(child.GetComponent<Tile>());
            GameObject pilon = Instantiate(m_PilonPrefab, child);
        }

        // Loop and set all materials and calculate tile neighbors
        foreach(Tile tile in m_TileList)
        {
            tile.GetComponent<Renderer>().material = m_LevelMaterial;

            
            /*
            Collider[] neighbors = Physics.OverlapSphere(tile.transform.position, 1.0f);
            for (int i = 0; i < neighbors.Length; i++)
            {
                Tile other = neighbors[i].gameObject.GetComponent<Tile>();

                if (other != null && !tile.m_NeighborList.Contains(other) && other != tile)
                {
                    tile.m_NeighborList.Add(other);
                }
            }
            */
        }
    }

    void CreatePath(int pathLength)
    {
        Tile startTile = m_TileList[Random.Range(0, m_TileList.Count - 1)];
        while (startTile.m_State != Tile.STATE.LOCKED)
            startTile = m_TileList[Random.Range(0, m_TileList.Count - 1)];

        StartCoroutine(PathEnumerator(pathLength, startTile));
    }

    IEnumerator PathEnumerator(int pathLength, Tile startTile)
    {
        Tile curTile = startTile;
        for(int i = 0; i < pathLength; ++i)
        {
            curTile.GetComponent<Renderer>().material = m_PathMaterial;

            curTile = curTile.m_NeighborList[Random.Range(0, curTile.m_NeighborList.Count - 1)];
            while (curTile.m_State != Tile.STATE.LOCKED)
                curTile = curTile.m_NeighborList[Random.Range(0, curTile.m_NeighborList.Count - 1)];

            yield return new WaitForSeconds(0.2f); 
        }
    }

    
}
