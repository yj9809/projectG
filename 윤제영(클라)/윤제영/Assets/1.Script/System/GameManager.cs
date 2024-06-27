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
    Counter,
    Non
}
public enum FunnitureType
{
    Table,
    Chair
}
public enum GameState
{
    Start,
    Stop
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform prfabPos;
    [SerializeField] private GameObject mainChar;
    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    public GameState gamestate = GameState.Start;

    private ObjData data;

    public NavMeshSurface nms;
    public Transform allChair;
    public Transform elementals;
    public GameObject buildingPrefab;
    public GameObject savePrefab;
    public Queue<GameObject> foods = new Queue<GameObject>();

    private Ui ui;
    public Ui Ui
    {
        get
        {
            if (ui == null)
                ui = FindObjectOfType<Ui>();
            return ui;
        }
    }

    private MainCharacter mainCharacter;
    public MainCharacter MainCharacter
    {
        get
        {
            if (mainCharacter == null)
                mainCharacter = FindObjectOfType<MainCharacter>();
            return mainCharacter;
        }
    }

    private House house;
    public House House
    {
        get
        {
            if (house == null)
                house = FindObjectOfType<House>();

            return house;
        }
    }

    private Transform counterPos;
    public Transform CounterPos
    {
        get 
        {
            if (counterPos == null)
                counterPos = GameObject.Find("Counter Pos").transform;

            return counterPos;
        }
    }

    private Transform protagonistPos;
    public Transform ProtagonistPos
    {
        get
        {
            if (protagonistPos == null)
                protagonistPos = GameObject.Find("Protagonist Pos").transform;

            return protagonistPos;
        }
    }

    private Transform kitchenPos;
    public Transform KitchenPos
    {
        get
        {
            if (kitchenPos == null)
                kitchenPos = GameObject.Find("Kitchen Pos").transform;
            return kitchenPos;
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

    private Spawn spawn;
    public Spawn Spawn
    {
        get
        {
            if (spawn == null)
            {
                spawn = FindObjectOfType<Spawn>();
            }
            return spawn;
        }
    }

    private ControlSkyBox sky;
    public ControlSkyBox Sky
    {
        get
        {
            if (sky == null)
                sky = FindObjectOfType<ControlSkyBox>();

            return sky;
        }
    }
    public int Happy
    {
        get { return data.happy; }
        set
        {
            data.happy = value;
            Ui.MainTxt();
        }
    }
    public int WheatEa
    {
        get { return data.wheatEa; }
        set
        {
            data.wheatEa = value;
            Ui.MainTxt();
        }
    }
    public int PotatoEa
    {
        get { return data.potatoEa; }
        set
        {
            data.potatoEa = value;
            Ui.MainTxt();
        }
    }
    public int TomatoEa
    {
        get { return data.tomatoEa; }
        set
        {
            data.tomatoEa = value;
            Ui.MainTxt();
        }
    }
    public int ButterMushroomEa
    {
        get { return data.butterMushroomEa; }
        set
        {
            data.butterMushroomEa = value;
            Ui.MainTxt();
        }
    }
    public int Day
    {
        get { return data.day; }
        set
        {
            data.day = value;
        }
    }
    public string PlayerName
    {
        get { return data.saveName; }
    }
    public int tileNum;
    public int funnitureNum;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        // SetElementals
        elementals = GameObject.Find("Elementals").transform;
        // SetActive
        SeletTile.SetActive(false);
        SeletFunniture.SetActive(false);
        TileGrid.SetActive(false);
        // Data
        data = DataManager.Instance.now;
        // Prefab
        prfabPos = GameObject.Find("PrefabPos").transform;

        GameObject prefab = Instantiate(buildingPrefab, prfabPos.position, Quaternion.identity);
        prefab.name = "Save Obj Prefab";
        allChair = prefab.transform.GetChild(2).transform;
        savePrefab = GameObject.Find("Save Obj Prefab");

        // Instantiate Main Character
        mainCharacter =
            Instantiate(mainChar, House.housePos.position,Quaternion.identity).GetComponent<MainCharacter>();

        // Nav
        nms = GetComponent<NavMeshSurface>();
        nms.BuildNavMesh();
        DataManager.Instance.SavePrefab(savePrefab);

        // Ui Set
        Ui.MainTxt();
        // Elementals initialization
        for (int i = 0; i < Spawn.elemental.Count; i++)
        {
            ElementalNpc elem = Spawn.elemental[i].GetComponent<ElementalNpc>();
            elem.GoStore();
        }

        // GameStart logic
        Spawn.customerList.Clear();
        House.partner.Clear();
        MainCharacter.GoFarm();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            // SetElementals
            elementals = GameObject.Find("Elementals").transform;
            // SetActive
            SeletTile.SetActive(false);
            SeletFunniture.SetActive(false);
            TileGrid.SetActive(false);
            // Data
            data = DataManager.Instance.now;
            // Prefab
            prfabPos = GameObject.Find("PrefabPos").transform;

            GameObject prefab = Instantiate(buildingPrefab, prfabPos.position, Quaternion.identity);
            prefab.name = "Save Obj Prefab";
            allChair = prefab.transform.GetChild(2).transform;
            savePrefab = GameObject.Find("Save Obj Prefab");

            // Instantiate Main Character
            mainCharacter =
                Instantiate(mainChar, House.housePos.position, Quaternion.identity).GetComponent<MainCharacter>();

            // Nav
            nms = GetComponent<NavMeshSurface>();
            nms.BuildNavMesh();
            DataManager.Instance.SavePrefab(savePrefab);

            // Ui Set
            Ui.MainTxt();

            // Elementals initialization
            for (int i = 0; i < Spawn.elemental.Count; i++)
            {
                ElementalNpc elem = Spawn.elemental[i].GetComponent<ElementalNpc>();
                elem.GoStore();
            }

            // GameStart logic
            Spawn.customerList.Clear();
            House.partner.Clear();
            MainCharacter.GoFarm();
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
