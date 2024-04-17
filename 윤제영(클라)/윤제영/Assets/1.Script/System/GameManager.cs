using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjType
{
    Tile,
    Wall,
    Non
}
public class GameManager : Singleton<GameManager>
{
    public int tileNum;
    public ObjType oType = ObjType.Non;
    private GameObject tileGrid;
    public GameObject TileGrid
    {
        get
        {
            if (tileGrid == null)
            {
                tileGrid = GameObject.Find("Tile Grid");
            }
            return tileGrid;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
