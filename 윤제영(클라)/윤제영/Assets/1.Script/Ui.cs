using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class Ui : MonoBehaviour
{
    //DestroyWindow
    [SerializeField] private GameObject destroy;
    [SerializeField] private GameObject destryoCloseButton;
    //SeletWindow
    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;
    [SerializeField] private GameObject buttons;
    //closing
    [SerializeField] private Image closing;
    [SerializeField] private Image point;
    [SerializeField] private Image food;
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
    [SerializeField] private Image clockForwordGround;
    //Ui Txt
    [SerializeField] private TMP_Text happyPointTxt;
    [SerializeField] private TMP_Text[] cropTxt;

    private GameManager gm;

    private int timeScaleValue = 0;
    private int time;
    public int happyUp = 0;
    public int happyDown = 0;
    public int[] cropOneDayEa = new int[4];

    private bool tileWindow = false;
    private bool funnitureWindow = false;
    private bool destroySystem = false;
    private bool onClosing = false;
    private void Awake()
    {
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeScaleChange();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0;
        }
    }
    public void OnStart()
    {
        GameManager.Instance.gamestate = GameState.Start;
    }
    public void TileWindow()
    {
        if (!funnitureWindow && !destroySystem)
        {
            
            if (!tileWindow)
            {
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(1900, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(true);
                gm.SeletTile.SetActive(true);
                time = timeScaleValue -1;
                timeScaleValue = 2;
                TimeScaleChange();
                buttons.transform.DOMoveX(2120, 1f).SetUpdate(true);
                tileWindow = true;
            }
            else if (tileWindow)
            {
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(false);
                gm.SeletTile.SetActive(false);
                tileWindow = false;
                timeScaleValue = time;
                TimeScaleChange();
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
            }
        }
    }
    public void FunnitureWindow()
    {
        if (!tileWindow && !destroySystem)
        {
            if (!funnitureWindow)
            {
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(1900, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(true);
                gm.SeletFunniture.SetActive(true);
                time = timeScaleValue - 1;
                timeScaleValue = 2;
                TimeScaleChange();
                buttons.transform.DOMoveX(2120, 1f).SetUpdate(true);
                funnitureWindow = true;
            }
            else if (funnitureWindow)
            {
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(false);
                gm.SeletFunniture.SetActive(false);
                funnitureWindow = false;
                timeScaleValue = time;
                TimeScaleChange();
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
            }
        }
        
    }
    public void OnDestroySystem()
    {
        if (!tileWindow && ! funnitureWindow)
        {
            if (!destroySystem)
            {
                destryoCloseButton.transform.DOMoveX(1900, 1f).SetUpdate(true);
                buttons.transform.DOMoveX(2120, 1f).SetUpdate(true);
                destroy.SetActive(true);
                time = timeScaleValue - 1;
                timeScaleValue = 2;
                TimeScaleChange();
                destroySystem = true;
            }
            else if (destroySystem)
            {
                destryoCloseButton.transform.DOMoveX(2220, 1f).SetUpdate(true);
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
                destroy.SetActive(false);
                destroySystem = false;
                timeScaleValue = time;
                TimeScaleChange();
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

        for (int i = 0; i < cropOneDayEa.Length; i++)
        {
            if (cropOneDayEa[i] != 0)
                closingCropTxt[i].text = $" <color=green> + {cropOneDayEa[i]}</color>";
            else
                closingCropTxt[i].text = cropOneDayEa[i].ToString();
        }

        if (!onClosing)
        {
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
        switch(num)
        {
            case 0:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                break;
            case 1:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                break;
            case 2:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                break;
            case 3:
                for (int i = 0; i < 20; i++)
                    gm.foods.Enqueue(foodPrefab[num]);
                break;
        }
    }
}
