using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TileType
{
    Tile,
    Wall,
    Non
}
public enum FunnitureType
{
    Table,
    Chair
}
public class GameManager : Singleton<GameManager>
{
    public GameObject seletTile;
    public GameObject seletFunniture;

    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    public Transform tWParent;
    public Transform tableParent;
    public Transform chairParent;
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
    public int tileNum;
    // Start is called before the first frame update
    void Start()
    {
        // Selet Obj
        seletTile = GameObject.Find("[ SeletTile ]");
        seletFunniture = GameObject.Find("[ SeletFunniture ]");
        // Parent
        tWParent = GameObject.Find("Tile/Wall Parent").transform;
        tableParent = GameObject.Find("Table Parent").transform;
        chairParent = GameObject.Find("Chair Parent").transform;
        //SetActive
        seletTile.SetActive(false);
        seletFunniture.SetActive(false);
        TileGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
