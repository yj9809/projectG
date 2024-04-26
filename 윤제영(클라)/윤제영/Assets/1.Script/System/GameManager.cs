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
    [SerializeField] private GameObject test;

    public GameObject seletTile;
    public GameObject seletFunniture;

    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    public GameObject tileParent;
    public GameObject tableParent;
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
    private void Awake()
    {
        tableParent = GameObject.Find("Table Parent");
        tileParent = GameObject.Find("Tile Parent");
        seletTile = GameObject.Find("[ SeletTile ]");
        seletFunniture = GameObject.Find("[ SeletFunniture ]");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(test.transform.localPosition);
    }
    public void OnLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
