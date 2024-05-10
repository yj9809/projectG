using System.Collections;
using System.Collections.Generic;
using System;
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
    private ObjData data;

    private GameObject seletTile;
    public GameObject SeletTile
    {
        get
        {
            if (seletTile == null)
            {
                seletTile = GameObject.Find("[ SeletTile ]");
            }
            return seletTile;
        }
    }
    private GameObject seletFunniture;
    public GameObject SeletFunniture
    {
        get
        {
            if (seletFunniture == null)
            {
                seletFunniture = GameObject.Find("[ SeletFunniture ]");
            }
            return seletFunniture;
        }
    }
    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    private Transform twParent;
    public Transform tWParent
    {
        get
        {
            if (twParent == null)
            {
                twParent = GameObject.Find("Tile/Wall Parent").transform;
            }
            return twParent;
        }
    }
    private Transform tableParent;
    public Transform TableParent
    {
        get
        {
            if (tableParent == null)
            {
                tableParent = GameObject.Find("Table Parent").transform;
            }
            return tableParent;
        }
    }
    private Transform chairParent;
    public Transform ChairParent
    {
        get
        {
            if (chairParent == null)
            {
                chairParent = GameObject.Find("Chair Parent").transform;
            }
            return chairParent;
        }
    }
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
        SceneManager.sceneLoaded += OnSceneLoaded;
        SeletTile.SetActive(false);
        SeletFunniture.SetActive(false);
        TileGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            //SetActive
            SeletTile.SetActive(false);
            SeletFunniture.SetActive(false);
            TileGrid.SetActive(false);
            //Data
            data = DataManager.Instance.now;
            Instantiate(data.chairParent);
        }
    }
    public void OnLoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnSave()
    {
        data.tWParent = this.tWParent.gameObject;
        data.tableParent = this.tableParent.gameObject;
        data.chairParent = this.chairParent.gameObject;
        data.time = DateTime.Now.ToString("yyyy-MM-dd");
        DataManager.Instance.SaveData();
    }
}
