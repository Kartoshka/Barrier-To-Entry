using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public List<Tile> m_TileList;
    public Material m_LevelMaterial;

	// Use this for initialization
	void Start () {
        LoadTileList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadTileList()
    {
        // Instantiate new TileList
        m_TileList = new List<Tile>(transform.childCount);

        // Loop and add all tiles
        foreach (Transform child in transform)
        {
            Debug.Log("IN FOR EACH");
            m_TileList.Add(child.GetComponent<Tile>());
        }

        foreach(Tile tile in m_TileList)
        {
            tile.GetComponent<Renderer>().material = m_LevelMaterial;
        }
    }
}
