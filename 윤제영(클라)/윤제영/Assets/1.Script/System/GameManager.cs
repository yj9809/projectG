using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public enum CreatType
{
    New,
    Load
}
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
    [SerializeField] private GameObject[] obj;
    [SerializeField] private GameObject option;
    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    public GameState gamestate = GameState.Start;

    private OptionManager om;
    private ObjData data;
    private Dictionary<string, GameObject> prefab;
    private CreatType ct = CreatType.New;

    public NavMeshSurface nms;
    public Transform allTw;
    public Transform allTable;
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
    public int AllHappy
    {
        get { return data.allHappyPoint; }
        set
        {
            data.allHappyPoint = value;
            UpdateStep();
            UpdateSpawnTimer();
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
    public int Step
    {
        get { return data.tileGridStep; }
        private set
        {
            data.tileGridStep = Math.Clamp(value, 0, 3);
        }
    }
    public string PlayerName
    {
        get { return data.saveName; }
    }
    public int CounteEa
    {
        get { return data.counteEa; }
        set
        {
            data.counteEa = value;
        }
    }
    public int Timer
    {
        get { return data.timer; }
        private set
        {
            data.timer = Math.Clamp(value, 0, 4);
        }
    }
    public int tileNum;
    public int funnitureNum;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        prefab = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in obj)
        {
            this.prefab[prefab.name] = prefab;
        }
        om = OptionManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBgm(AudioManager.Bgm.Main);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            AudioManager.Instance.PlayBgm(AudioManager.Bgm.Game);
            // SetElementals
            elementals = GameObject.Find("Elementals").transform;
            // SetActive
            SeletTile.SetActive(false);
            SeletFunniture.SetActive(false);
            //TileGrid.SetActive(false);
            for (int i = 0; i < Ui.tileGrid.Length; i++)
            {
                Ui.tileGrid[i].SetActive(false);
                Ui.InteriorGrid[i].SetActive(false);
            }
            // Data
            data = DataManager.Instance.now;
            // Prefab
            prfabPos = GameObject.Find("PrefabPos").transform;

            GameObject prefab = Instantiate(buildingPrefab, prfabPos.position, Quaternion.identity);
            prefab.name = "Save Obj Prefab";
            allTw = prefab.transform.GetChild(0).transform;
            allTable = prefab.transform.GetChild(1).transform;
            allChair = prefab.transform.GetChild(2).transform;
            savePrefab = GameObject.Find("Save Obj Prefab");
            Debug.Log("½ÇÇà");
            if (ct == CreatType.Load)
                LoadObj();

            // Instantiate Main Character
            mainCharacter =
                Instantiate(mainChar, House.housePos.position, Quaternion.identity).GetComponent<MainCharacter>();

            // Nav
            nms = GetComponent<NavMeshSurface>();
            nms.BuildNavMesh();

            // Ui Set
            GameObject optionWindow = Instantiate(option, Ui.uiCanvas.transform);
            Ui.OptionSet(optionWindow);
            Ui.MainTxt();
            UpdateStep();
            UpdateSpawnTimer();
            OptionWindowSet();
            optionWindow.SetActive(false);

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
    private void UpdateStep()
    {
        int newStep = data.allHappyPoint / 1000;
        Step = newStep;
    }
    public void UpdateSpawnTimer()
    {
        int newTime =data.allHappyPoint / 1000;
        Timer = newTime;
    }
    public void OnLoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnSave()
    {
        SaveObj();
        DataManager.Instance.SaveData();
    }
    public void SaveObj()
    {
        DataClear();
        for (int i = 0; i < allTw.childCount; i++)
        {
            data.twName.Add(allTw.GetChild(i).name);
            data.twPosition.Add(allTw.GetChild(i).transform.position);
            data.twRotation.Add(allTw.GetChild(i).transform.rotation);
        }
        for (int i = 0; i < allTable.childCount; i++)
        {
            data.tableName.Add(allTable.GetChild(i).name);
            data.tablePosition.Add(allTable.GetChild(i).transform.position);
            data.tableRotation.Add(allTable.GetChild(i).transform.rotation);
        }
        for (int i = 0; i < allChair.childCount; i++)
        {
            data.chairName.Add(allChair.GetChild(i).name);
            data.chairPosition.Add(allChair.GetChild(i).transform.position);
            data.chairRotation.Add(allChair.GetChild(i).transform.rotation);
        }
    }
    public void LoadObj()
    {
        for (int i = 0; i < data.twName.Count; i++)
        {
            string name = data.twName[i];
            if (prefab.ContainsKey(name))
            {
                GameObject pref = prefab[name];
                GameObject obj = Instantiate(pref, data.twPosition[i], data.twRotation[i], allTw);
                obj.name = name;
            }
        }
        for (int i = 0; i < data.tableName.Count; i++)
        {
            string name = data.tableName[i];
            if (prefab.ContainsKey(name))
            {
                GameObject pref = prefab[name];
                GameObject obj = Instantiate(pref, data.tablePosition[i], data.tableRotation[i], allTable);
                obj.name = name;
            }
        }
        for (int i = 0; i < data.chairName.Count; i++)
        {
            string name = data.chairName[i];
            if (prefab.ContainsKey(name))
            {
                GameObject pref = prefab[name];
                GameObject obj = Instantiate(pref, data.chairPosition[i], data.chairRotation[i], allChair);
                obj.name = name;
            }
        }
    }
    private void DataClear()
    {
        data.twName.Clear();
        data.twPosition.Clear();
        data.twRotation.Clear();
        data.tableName.Clear();
        data.tablePosition.Clear();
        data.tableRotation.Clear();
        data.chairName.Clear();
        data.chairPosition.Clear();
        data.chairRotation.Clear();
    }
    private void OptionWindowSet()
    {
        om.resolutionDropdown = GameObject.Find("Resolution Dropdown").transform.GetComponent<TMP_Dropdown>();
        om.fullScreenToggle = GameObject.Find("Full").transform.GetComponent<Toggle>();
        om.masterVolumeSlider = GameObject.Find("MasterVolume").transform.GetComponent<Slider>();
        om.bgmVolumeSlider = GameObject.Find("Bgm Volume").transform.GetComponent<Slider>();
        om.sfxVolumeSlider = GameObject.Find("Sfx Volume").transform.GetComponent<Slider>();
        om.OptionValueSet();
    }
    public void CreatTypeChange(string type)
    {
        if (type == "Load")
            ct = CreatType.Load;
    }
}
