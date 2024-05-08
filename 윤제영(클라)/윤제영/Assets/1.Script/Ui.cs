using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ui : MonoBehaviour
{
    [SerializeField] private Image tileSeletWindow;
    [SerializeField] private Image funnitureSeletWindow;
    private GameManager gm;

    private bool tileWindow;
    private bool funnitureWindow;
    private void Start()
    {
        tileWindow = false;
        funnitureWindow = false;
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        
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
    public void OnSave()
    {
        GameManager.Instance.OnSave();
    }
}
