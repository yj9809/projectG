using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
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
    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;

    [SerializeField] private Transform prfabPos;
    
    private ObjData data;

    public NavMeshSurface nms;
    public Transform allChair;
    public Transform elementals;
    public GameObject buildingPrefab;
    public GameObject savePrefab;

    private Transform counterPos;
    public Transform CounterPos
    {
        get 
        {
            if (counterPos == null)
            {
                counterPos = GameObject.Find("Counter Pos").transform;
            }
            return counterPos;
        }
    }

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
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //GameObject prefab = Instantiate(nomalPrefab, prfabPos.position, Quaternion.identity);
        //prefab.name = "Save Obj Prefab";
        //savePrefab = GameObject.Find("Save Obj Prefab");

        //nms = GetComponent<NavMeshSurface>();
        //nms.BuildNavMesh();

        //SeletTile.SetActive(false);
        //SeletFunniture.SetActive(false);
        //TileGrid.SetActive(false);

        //SetElementals
        elementals = GameObject.Find("Elementals").transform;
        //SetActive
        SeletTile.SetActive(false);
        SeletFunniture.SetActive(false);
        TileGrid.SetActive(false);
        //Data
        data = DataManager.Instance.now;
        //Prefab
        prfabPos = GameObject.Find("PrefabPos").transform;
        GameObject prefab = Instantiate(buildingPrefab, prfabPos.position, Quaternion.identity);
        prefab.name = "Save Obj Prefab";
        allChair = prefab.transform.GetChild(2).transform;
        savePrefab = GameObject.Find("Save Obj Prefab");
        //Nav
        nms = GetComponent<NavMeshSurface>();
        nms.BuildNavMesh();
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
            //Prefab
            prfabPos = GameObject.Find("PrefabPos").transform;
            GameObject prefab = Instantiate(buildingPrefab, prfabPos.position, Quaternion.identity);
            prefab.name = "Save Obj Prefab";
            allChair = prefab.transform.GetChild(2).transform;
            savePrefab = GameObject.Find("Save Obj Prefab");
            //Nav
            nms = GetComponent<NavMeshSurface>();
            nms.BuildNavMesh();
        }
    }
    public void OnLoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnSave()
    {
        DataManager.Instance.SavePrefab(savePrefab);
        DataManager.Instance.SaveData();
    }
    

}
