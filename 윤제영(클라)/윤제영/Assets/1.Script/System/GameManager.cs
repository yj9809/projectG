using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public enum CreatType { New, Load }
public enum TileType { Tile, Wall, Counter, Non }
public enum FunnitureType { Table, Chair }
public enum GameState { Start, Stop }

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform prefabPos;
    [SerializeField] private GameObject mainChar;
    [SerializeField] private GameObject[] obj;
    [SerializeField] private GameObject option;

    public TileType oType = TileType.Non;
    public FunnitureType fType = FunnitureType.Table;
    public GameState gameState = GameState.Start;

    private OptionManager om;
    private ObjData data;
    private Dictionary<string, GameObject> prefabDict;
    private CreatType creatType = CreatType.New;

    public NavMeshSurface navMeshSurface;
    public Transform allTw;
    public Transform allTable;
    public Transform allChair;
    public Transform elementals;
    public GameObject buildingPrefab;
    public GameObject savePrefab;
    public Queue<GameObject> foods = new Queue<GameObject>();

    private Ui ui;
    private MainCharacter mainCharacter;
    private House house;
    private Spawn spawn;
    private ControlSkyBox sky;

    // Properties for commonly used objects and values
    public Ui Ui => ui ??= FindObjectOfType<Ui>();
    public MainCharacter MainCharacter => mainCharacter ??= FindObjectOfType<MainCharacter>();
    public House House => house ??= FindObjectOfType<House>();
    public Spawn Spawn => spawn ??= FindObjectOfType<Spawn>();
    public ControlSkyBox Sky => sky ??= FindObjectOfType<ControlSkyBox>();

    // Cached Transform references
    private Transform counterPos;
    private Transform protagonistPos;
    private Transform kitchenPos;
    private Transform twParent;
    private Transform tableParent;
    private Transform chairParent;
    private GameObject selectTile;
    private GameObject selectFurniture;

    public Transform CounterPos => counterPos ??= GameObject.Find("Counter Pos").transform;
    public Transform ProtagonistPos => protagonistPos ??= GameObject.Find("Protagonist Pos").transform;
    public Transform KitchenPos => kitchenPos ??= GameObject.Find("Kitchen Pos").transform;
    public Transform TWParent => twParent ??= GameObject.Find("Tile/Wall Parent").transform;
    public Transform TableParent => tableParent ??= GameObject.Find("Table Parent").transform;
    public Transform ChairParent => chairParent ??= GameObject.Find("Chair Parent").transform;
    public GameObject SelectTile => selectTile ??= GameObject.Find("[ SeletTile ]");
    public GameObject SelectFurniture => selectFurniture ??= GameObject.Find("[ SeletFunniture ]");

    public int AllHappy
    {
        get => data.allHappyPoint;
        set
        {
            data.allHappyPoint = value;
            UpdateStep();
            UpdateSpawnTimer();
        }
    }
    public int Happy
    {
        get => data.happy;
        set
        {
            data.happy = value;
            Ui.MainTxt();
        }
    }
    public int WheatEa
    {
        get => data.wheatEa;
        set
        {
            data.wheatEa = value;
            Ui.MainTxt();
        }
    }
    public int PotatoEa
    {
        get => data.potatoEa;
        set
        {
            data.potatoEa = value;
            Ui.MainTxt();
        }
    }
    public int TomatoEa
    {
        get => data.tomatoEa;
        set
        {
            data.tomatoEa = value;
            Ui.MainTxt();
        }
    }
    public int ButterMushroomEa
    {
        get => data.butterMushroomEa;
        set
        {
            data.butterMushroomEa = value;
            Ui.MainTxt();
        }
    }
    public int Day
    {
        get => data.day;
        set => data.day = value;
    }
    public int Step
    {
        get => data.tileGridStep;
        private set => data.tileGridStep = Mathf.Clamp(value, 0, 3);
    }
    public string PlayerName => data.saveName;
    public int CounteEa
    {
        get => data.counteEa;
        set => data.counteEa = value;
    }
    public int Timer
    {
        get => data.timer;
        private set => data.timer = Mathf.Clamp(value, 0, 4);
    }

    public int TileNum { get; set; }
    public int FurnitureNum { get; set; }

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        prefabDict = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in obj)
        {
            prefabDict[prefab.name] = prefab;
        }
        om = OptionManager.Instance;
    }

    void Start()
    {
        AudioManager.Instance.PlayBgm(AudioManager.Bgm.Main);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Game") return;

        AudioManager.Instance.PlayBgm(AudioManager.Bgm.Game);
        InitializeScene();
        InitializeUI();
        InstantiateMainCharacter();
        InitializeNavMesh();
        InitializeElementals();
        StartGame();
    }

    private void InitializeScene()
    {
        elementals = GameObject.Find("Elementals").transform;
        SelectTile.SetActive(false);
        SelectFurniture.SetActive(false);
        foreach (var tile in Ui.tileGrid) tile.SetActive(false);
        foreach (var interior in Ui.InteriorGrid) interior.SetActive(false);

        data = DataManager.Instance.now;
        prefabPos = GameObject.Find("PrefabPos").transform;

        GameObject prefab = Instantiate(buildingPrefab, prefabPos.position, Quaternion.identity);
        prefab.name = "Save Obj Prefab";
        allTw = prefab.transform.GetChild(0);
        allTable = prefab.transform.GetChild(1);
        allChair = prefab.transform.GetChild(2);
        savePrefab = GameObject.Find("Save Obj Prefab");

        if (creatType == CreatType.Load)
            LoadObj();
    }

    private void InitializeUI()
    {
        GameObject optionWindow = Instantiate(option, Ui.uiCanvas.transform);
        Ui.OptionSet(optionWindow);
        Ui.MainTxt();
        UpdateStep();
        UpdateSpawnTimer();
        OptionWindowSet();
        optionWindow.SetActive(false);
    }
    private void InstantiateMainCharacter()
    {
        mainCharacter = Instantiate(mainChar, House.housePos.position, Quaternion.identity).GetComponent<MainCharacter>();
    }
    private void InitializeNavMesh()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }
    private void InitializeElementals()
    {
        foreach (var elemental in Spawn.elemental)
        {
            ElementalNpc elem = elemental.GetComponent<ElementalNpc>();
            elem.GoStore();
        }
    }
    private void StartGame()
    {
        Spawn.customerList.Clear();
        House.partner.Clear();
        MainCharacter.GoFarm();
    }

    private void UpdateStep() => Step = data.allHappyPoint / 1000;

    public void UpdateSpawnTimer() => Timer = data.allHappyPoint / 1000;

    public void OnLoadScene() => SceneManager.LoadScene("Game");

    public void OnSave()
    {
        SaveObj();
        DataManager.Instance.SaveData();
    }

    public void SaveObj()
    {
        DataClear();
        SaveTransforms(allTw, data.twName, data.twPosition, data.twRotation);
        SaveTransforms(allTable, data.tableName, data.tablePosition, data.tableRotation);
        SaveTransforms(allChair, data.chairName, data.chairPosition, data.chairRotation);
    }

    private void SaveTransforms(Transform parent, List<string> names, List<Vector3> positions, List<Quaternion> rotations)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            names.Add(child.name);
            positions.Add(child.position);
            rotations.Add(child.rotation);
        }
    }

    public void LoadObj()
    {
        LoadTransforms(data.twName, data.twPosition, data.twRotation, allTw);
        LoadTransforms(data.tableName, data.tablePosition, data.tableRotation, allTable);
        LoadTransforms(data.chairName, data.chairPosition, data.chairRotation, allChair);
    }

    private void LoadTransforms(List<string> names, List<Vector3> positions, List<Quaternion> rotations, Transform parent)
    {
        for (int i = 0; i < names.Count; i++)
        {
            string name = names[i];
            if (prefabDict.TryGetValue(name, out GameObject prefab))
            {
                Instantiate(prefab, positions[i], rotations[i], parent).name = name;
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
        om.resolutionDropdown = GameObject.Find("Resolution Dropdown").GetComponent<TMP_Dropdown>();
        om.fullScreenToggle = GameObject.Find("Full").GetComponent<Toggle>();
        om.masterVolumeSlider = GameObject.Find("MasterVolume").GetComponent<Slider>();
        om.bgmVolumeSlider = GameObject.Find("Bgm Volume").GetComponent<Slider>();
        om.sfxVolumeSlider = GameObject.Find("Sfx Volume").GetComponent<Slider>();
        om.OptionValueSet();
    }

    public void CreatTypeChange(string type)
    {
        if (type == "Load")
            creatType = CreatType.Load;
    }
}
