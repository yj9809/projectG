using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ui : MonoBehaviour
{
    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;
    [SerializeField] private Button next;
    [SerializeField] private GameObject destroy;
    private GameManager gm;

    private bool tileWindow;
    private bool funnitureWindow;
    private bool destroySystem;
    private void Start()
    {
        tileWindow = false;
        funnitureWindow = false;
        destroySystem = false;
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
        if (!funnitureWindow)
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
        if (!tileWindow)
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
    public void OnDestroySystem()
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
    public void OnSave()
    {
        GameManager.Instance.OnSave();
    }
    public void TimeScaleChange(int speed)
    {
        switch (speed)
        {
            case 1:
                Time.timeScale = 1f;
                break;
            case 2:
                Time.timeScale = 2f;
                break;
            case 3:
                Time.timeScale = 3f;
                break;
        }
    }
    public void OnNextButton()
    {
        next.gameObject.SetActive(true);
    }
}
