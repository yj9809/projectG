using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Ui : MonoBehaviour
{
    [SerializeField] private GameObject destroy;
    [SerializeField] private GameObject destryoCloseButton;

    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;
    [SerializeField] private GameObject buttons;

    [SerializeField] private Button next;

    [SerializeField] private Button timeScaleButton;
    [SerializeField] private Sprite[] timeScaleSprite;

    [SerializeField] private Image clockBackGround;

    [SerializeField] private TMP_Text happyPointTxt;

    private GameManager gm;

    private int timeScaleValue = 0;
    private bool tileWindow = false;
    private bool funnitureWindow = false;
    private bool destroySystem = false;
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
                buttons.transform.DOMoveX(2120, 1f).SetUpdate(true);
                tileWindow = true;
            }
            else if (tileWindow)
            {
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(false);
                gm.SeletTile.SetActive(false);
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
                tileWindow = false;
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
                buttons.transform.DOMoveX(2120, 1f).SetUpdate(true);
                funnitureWindow = true;
            }
            else if (funnitureWindow)
            {
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveX(2520, 1f).SetUpdate(true);
                gm.TileGrid.SetActive(false);
                gm.SeletFunniture.SetActive(false);
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
                funnitureWindow = false;
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
                destroySystem = true;
            }
            else if (destroySystem)
            {
                destryoCloseButton.transform.DOMoveX(2220, 1f).SetUpdate(true);
                buttons.transform.DOMoveX(1920, 1f).SetUpdate(true);
                destroy.SetActive(false);
                destroySystem = false;
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
        timeScaleValue++;

        switch (timeScaleValue)
        {
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
    public void OnNextButton()
    {
            next.gameObject.SetActive(true);
    }
    public void RotationClockHand(float time)
    {
        float rotationZ = Mathf.Lerp(0f, -180f, time);

        clockBackGround.rectTransform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
    public void HappyPoint()
    {
        happyPointTxt.text = $"{gm.Happy}";
    }
}
