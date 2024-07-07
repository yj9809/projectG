using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum MenuType
{
    OnMenu,
    OffMenu
}
public class Ui : MonoBehaviour
{
    //Canvas
    public Canvas uiCanvas;
    //TileGrid
    public GameObject[] tileGrid;
    public GameObject[] InteriorGrid;
    //DestroyWindow
    [SerializeField] private GameObject destroy;
    [SerializeField] private GameObject destryoCloseButton;
    public Button destroyExecution;
    public TMP_Text destroyExecutionEa;
    //SeletWindow
    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;
    [SerializeField] private GameObject buttons;
    //closing
    [SerializeField] private Image closing;
    [SerializeField] private Image point;
    [SerializeField] private Image food;
    [SerializeField] private TMP_Text step;
    [SerializeField] private TMP_Text closingMainTitle;
    [SerializeField] private TMP_Text closingTitleDay;
    [SerializeField] private TMP_Text closingHappyTxt;
    [SerializeField] private TMP_Text[] closingCropTxt;
    //Food Station
    [System.Serializable]
    public class Food
    {
        public TMP_Text[] ingredient;
        public Button preparation;
    }
    [SerializeField] private List<Food> foodStation;
    [SerializeField] private GameObject[] foodPrefab;
    //TimeScale
    [SerializeField] private Button timeScaleButton;
    [SerializeField] private Sprite[] timeScaleSprite;
    //Clock
    [SerializeField] private Image clockBackGround;
    //Ui Txt
    [SerializeField] private TMP_Text happyPointTxt;
    [SerializeField] private TMP_Text[] cropTxt;
    //Option
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject option;

    private GameManager gm;
    private MenuType mt = MenuType.OnMenu;

    public int timeScaleValue = 0;
    public int time;
    public int happyUp = 0;
    public int happyDown = 0;
    public int[] cropOneDayEa = new int[4];

    private bool tileWindow = false;
    private bool funnitureWindow = false;
    private bool destroySystem = false;
    private bool onClosing = false;
    private bool onMenuWindow = false;
    private bool onOption = false;
    private void Awake()
    {
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeScaleChangeButton();
        }
        OnMenu();
    }
    public void OnStart()
    {
        GameManager.Instance.gamestate = GameState.Start;
    }
    public void TileWindow()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Click);
        if (!funnitureWindow && !destroySystem && !onMenuWindow)
        {
            if (!tileWindow)
            {
                mt = MenuType.OffMenu;
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(1900, 0.25f).SetUpdate(true);
                OnTile(tileWindow, gm.Step);
                gm.SeletTile.SetActive(true);
                time = timeScaleValue -1;
                timeScaleValue = 2;
                TimeScaleChange();
                buttons.transform.DOMoveX(2120, 0.25f).SetUpdate(true)
                    .OnComplete(() => tileWindow = true);
            }
            else if (tileWindow)
            {
                mt = MenuType.OnMenu;
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 0.25f).SetUpdate(true);
                OnTile(tileWindow, gm.Step);
                gm.SeletTile.SetActive(false);
                buttons.transform.DOMoveX(1920, 0.25f).SetUpdate(true)
                    .OnComplete(() => {
                        tileWindow = false; 
                        timeScaleValue = time;
                        TimeScaleChange();
                    });
            }
        }
    }
    public void FunnitureWindow()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Click);
        if (!tileWindow && !destroySystem && !onMenuWindow)
        {
            if (!funnitureWindow)
            {
                mt = MenuType.OffMenu;
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(1900, 0.25f).SetUpdate(true);
                OnTile(funnitureWindow, gm.Step);
                gm.SeletFunniture.SetActive(true);
                time = timeScaleValue - 1;
                timeScaleValue = 2;
                TimeScaleChange();
                buttons.transform.DOMoveX(2120, 0.25f).SetUpdate(true)
                    .OnComplete(() => funnitureWindow = true);
            }
            else if (funnitureWindow)
            {
                mt = MenuType.OnMenu;
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 0.25f).SetUpdate(true);
                OnTile(funnitureWindow, gm.Step);
                gm.SeletFunniture.SetActive(false);
                funnitureWindow = false;
                buttons.transform.DOMoveX(1920, 0.25f).SetUpdate(true)
                    .OnComplete(() => { 
                        funnitureWindow = false;
                        timeScaleValue = time;
                        TimeScaleChange();
                    });
            }
        }
        
    }
    public void OnTile(bool on, int step)
    {
        if(!on)
            switch (step)
            {
                case 0:
                    tileGrid[step].SetActive(true);
                    InteriorGrid[step].SetActive(true);
                    break;
                case 1:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(true);
                        InteriorGrid[i].SetActive(true);
                    }
                    break;
                case 2:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(true);
                        InteriorGrid[i].SetActive(true);
                    }
                    break;
                case 3:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(true);
                        InteriorGrid[i].SetActive(true);
                    }
                    break;
            }
        else
            switch (step)
            {
                case 0:
                    tileGrid[step].SetActive(false);
                    InteriorGrid[step].SetActive(false);
                    break;
                case 1:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(false);
                        InteriorGrid[i].SetActive(false);
                    }
                    break;
                case 2:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(false);
                        InteriorGrid[i].SetActive(false);
                    }
                    break;
                case 3:
                    for (int i = 0; i < step + 1; i++)
                    {
                        tileGrid[i].SetActive(false);
                        InteriorGrid[i].SetActive(false);
                    }
                    break;
            }

    }
    public void OnDestroySystem()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Click);
        if (!tileWindow && ! funnitureWindow && !onMenuWindow)
        {
            if (!destroySystem)
            {
                mt = MenuType.OffMenu;
                destryoCloseButton.transform.DOMoveX(1900, 0.25f).SetUpdate(true);
                destroy.SetActive(true);
                time = timeScaleValue - 1;
                timeScaleValue = 2;
                TimeScaleChange();
                buttons.transform.DOMoveX(2120, 0.25f).SetUpdate(true)
                    .OnComplete(() => destroySystem = true);
            }
            else if (destroySystem)
            {
                mt = MenuType.OnMenu;
                destryoCloseButton.transform.DOMoveX(2220, 0.25f).SetUpdate(true);
                destroy.SetActive(false);
                buttons.transform.DOMoveX(1920, 0.25f).SetUpdate(true)
                    .OnComplete(() => { 
                        destroySystem = false;
                        timeScaleValue = time;
                        TimeScaleChange();
                    });
            }
        }
    }
    public void TileNum(int num)
    {
        GameManager.Instance.tileNum = num;
    }
    public void TileType(int num)
    {
        GameManager.Instance.oType = (TileType)num;
    }
    public void FunnitureNum(int num)
    {
        GameManager.Instance.funnitureNum = num;
    }
    public void FunnitureType(int num)
    {
        GameManager.Instance.fType = (FunnitureType)num;
        Destroy(GameManager.Instance.SeletFunniture.GetComponent<SeletFunniture>().funniturePreView);
    }
    public void OnSave()
    {
        GameManager.Instance.OnSave();
    }
    public void TimeScaleChangeButton()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.TimeScale);
        TimeScaleChange();
    }
    public void TimeScaleChange()
    {
        if (!tileWindow && !funnitureWindow && !destroySystem)
        {
            timeScaleValue++;

            switch (timeScaleValue)
            {
                case 0:
                    Time.timeScale = 1f;
                    timeScaleButton.image.sprite = timeScaleSprite[timeScaleValue];
                    break;
                case 1:
                    Time.timeScale = 2f;
                    timeScaleButton.image.sprite = timeScaleSprite[timeScaleValue];
                    break;
                case 2:
                    Time.timeScale = 3f;
                    timeScaleButton.image.sprite = timeScaleSprite[timeScaleValue];
                    break;
                case 3:
                    Time.timeScale = 0f;
                    timeScaleButton.image.sprite = timeScaleSprite[timeScaleValue];
                    break;
                case 4:
                    timeScaleValue = 0;
                    Time.timeScale = 1f;
                    timeScaleButton.image.sprite = timeScaleSprite[timeScaleValue];
                    break;
            }
        }
    }
    public void RotationClockHand(float time)
    {
        float rotationZ = Mathf.Lerp(0f, -180f, time);

        clockBackGround.rectTransform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
    public void MainTxt()
    {
        happyPointTxt.text = $"{gm.Happy}";
        cropTxt[0].text = gm.WheatEa.ToString();
        cropTxt[1].text = gm.PotatoEa.ToString();
        cropTxt[2].text = gm.TomatoEa.ToString();
        cropTxt[3].text = gm.ButterMushroomEa.ToString();
    }
    public void Closing()
    {
        closingMainTitle.text = gm.PlayerName;
        closingTitleDay.text = $"Day {gm.Day}";
        step.text = (gm.Step + 1).ToString();

        for (int i = 0; i < cropOneDayEa.Length; i++)
        {
            if (cropOneDayEa[i] != 0)
                closingCropTxt[i].text = $" <color=green> + {cropOneDayEa[i]}</color>";
            else
                closingCropTxt[i].text = cropOneDayEa[i].ToString();
        }

        if (!onClosing)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Day);
            gm.foods.Clear();
            closing.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetUpdate(true);
            int happySum = happyUp + happyDown;
            closingHappyTxt.text = happySum >= 0 ? $"<color=green>+ {happySum}</color>" : $"<color=red>= {happySum}</color>";
            onClosing = true;
        }
        else if(onClosing)
        {
            closing.transform.DOScale(new Vector3(0, 0, 0), 0.1f).SetUpdate(true);
            happyUp = 0;
            happyDown = 0;
            point.gameObject.SetActive(true);
            food.gameObject.SetActive(false);
            for (int i = 0; i < cropOneDayEa.Length; i++)
            {
                cropOneDayEa[i] = 0;
            }
            onClosing = false;
        }
    }
    public void FoodCheck()
    {
        for (int i = 0; i < foodStation.Count; i++)
        {
            switch (i)
            {
                case 0:
                    foodStation[0].ingredient[0].text = gm.WheatEa >= 2 ? "<color=green>: 2" : "<color=red>: 2";
                    foodStation[0].ingredient[1].text = gm.TomatoEa >= 5 ? "<color=green>: 5" : "<color=red>: 5";
                    if (gm.WheatEa >= 2 && gm.TomatoEa >= 5)
                        foodStation[0].preparation.interactable = true;
                    else
                        foodStation[0].preparation.interactable = false;
                    break;
                case 1:
                    foodStation[1].ingredient[0].text = gm.WheatEa >= 1 ? "<color=green>: 1" : "<color=red>: 1";
                    foodStation[1].ingredient[1].text = gm.PotatoEa >= 5 ? "<color=green>: 5" : "<color=red>: 5";
                    if (gm.WheatEa >= 1 && gm.PotatoEa >= 5)
                        foodStation[1].preparation.interactable = true;
                    else
                        foodStation[1].preparation.interactable = false;
                    break;
                case 2:
                    foodStation[2].ingredient[0].text = gm.WheatEa >= 10 ? "<color=green>: 10" : "<color=red>: 10";
                    if (gm.WheatEa >= 10)
                        foodStation[2].preparation.interactable = true;
                    else
                        foodStation[2].preparation.interactable = false;
                    break;
                case 3:
                    foodStation[3].ingredient[0].text = gm.WheatEa >= 10 ? "<color=green>: 10" : "<color=red>: 10";
                    foodStation[3].ingredient[1].text = gm.ButterMushroomEa >= 3 ? "<color=green>: 3" : "<color=red>: 3";
                    if (gm.WheatEa >= 10 && gm.ButterMushroomEa >= 3)
                        foodStation[3].preparation.interactable = true;
                    else
                        foodStation[3].preparation.interactable = false;
                    break;
            }
        }
    }
    public void FoodSet(int num)
    {
        switch (num)
        {
            case 0:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                gm.WheatEa -= 2;
                gm.TomatoEa -= 5;
                FoodCheck();
                break;
            case 1:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                gm.WheatEa -= 1;
                gm.PotatoEa -= 5;
                FoodCheck();
                break;
            case 2:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                gm.WheatEa -= 10;
                FoodCheck();
                break;
            case 3:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                gm.WheatEa -= 10;
                gm.ButterMushroomEa -= 3;
                FoodCheck();
                break;
        }
    }
    public void TimeScaleSound()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.TimeScale);
    }
    private void OnMenu()
    {
        if (!destroySystem && !tileWindow && !funnitureWindow && mt == MenuType.OnMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !onMenuWindow)
            {
                menu.SetActive(true);
                onMenuWindow = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && onMenuWindow)
            {
                menu.SetActive(false);
                onMenuWindow = false;
            }
        }
    }
    public void OnOptionWindow()
    {
        if (!onOption)
        {
            option.SetActive(true);
            onOption = true;
        }
        else if (onOption)
        {
            option.SetActive(false);
            onOption = false;
        }
    }
    public void Exit()
    {
        OptionManager.Instance.OnExit();
    }
    public void OptionSet(GameObject option)
    {
        this.option = option;
    }
}
