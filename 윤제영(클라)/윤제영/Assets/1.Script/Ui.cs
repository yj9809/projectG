using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ui : MonoBehaviour
{
    [SerializeField] private GameObject destroy;

    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;

    [SerializeField] private Button next;

    [SerializeField] private Button timeScaleButton;
    [SerializeField] private Sprite[] timeScaleSprite;

    private GameManager gm;

    private int timeScaleValue = 0;
    private bool tileWindow = false;
    private bool funnitureWindow = false;
    private bool destroySystem = false;
    private void Start()
    {
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        
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
                    GetComponent<RectTransform>().DOMoveY(0, 1f);
                gm.TileGrid.SetActive(true);
                gm.SeletTile.SetActive(true);
                tileWindow = true;
            }
            else if (tileWindow)
            {
                tileSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveY(-240, 1f);
                gm.TileGrid.SetActive(false);
                gm.SeletTile.SetActive(false);
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
                    GetComponent<RectTransform>().DOMoveY(0, 1f);
                gm.TileGrid.SetActive(true);
                gm.SeletFunniture.SetActive(true);
                funnitureWindow = true;
            }
            else if (funnitureWindow)
            {
                funnitureSeletWindow.transform.
                    GetComponent<RectTransform>().DOMoveY(-240, 1f);
                gm.TileGrid.SetActive(false);
                gm.SeletFunniture.SetActive(false);
                funnitureWindow = false;
            }
        }
        
    }
    public void OnDestroySystem()
    {
        if (!tileWindow && ! funnitureWindow)
        {
            if (destroySystem)
            {
                destroy.SetActive(false);
                destroySystem = false;
            }
            else if (!destroySystem)
            {
                destroy.SetActive(true);
                destroySystem = true;
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
    //public void RotationClockHand(float time)
    //{
    //    float rotationZ = Mathf.Lerp(-70f, 70, time);

    //    clockHand.rectTransform.rotation = Quaternion.Euler(0, 0, rotationZ);
    //}
}
