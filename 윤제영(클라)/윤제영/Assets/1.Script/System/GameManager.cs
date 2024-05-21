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

    [SerializeField] private GameObject nomalPrefab;
    [SerializeField] private GameObject savePrefab;
    [SerializeField] private Transform prfabPos;

    public NavMeshSurface nms;

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
        GameObject prefab = Instantiate(nomalPrefab, prfabPos.position, Quaternion.identity);
        prefab.name = "Save Obj Prefab";
        savePrefab = GameObject.Find("Save Obj Prefab");

        nms = GetComponent<NavMeshSurface>();
        nms.BuildNavMesh();

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
        }
    }
    public void OnLoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnSave()
    {
        SavePrefab(savePrefab);
        DataManager.Instance.SaveData();
    }
    void SavePrefab(GameObject temp)
    {
        string fileName = "Save Obj Prefab";
        string path = "Assets/2.Prefab/Test/1/" + fileName + ".prefab";

        bool isSuccess = false;
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(temp, path, out isSuccess);// ¿˙¿Â
        Debug.Log(isSuccess);
        //UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
    }

}
