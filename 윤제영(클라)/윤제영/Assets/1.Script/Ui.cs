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
        gm.TileGrid.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TileWindow()
    {
        if (!tileWindow)
        {
            tileSeletWindow.transform.
                GetComponent<RectTransform>().DOMoveY(0 , 1f);
            gm.TileGrid.SetActive(true);
            tileWindow = true;
        }
        else if (tileWindow)
        {
            tileSeletWindow.transform.
                GetComponent<RectTransform>().DOMoveY(-240, 1f);
            gm.TileGrid.SetActive(false);
            tileWindow = false;
        }
    }
    public void FunnitureWindow()
    {
        if (!funnitureWindow)
        {
            funnitureSeletWindow.transform.
                GetComponent<RectTransform>().DOMoveY(0, 1f);
            gm.TileGrid.SetActive(true);
            funnitureWindow = true;
        }
        else if (funnitureWindow)
        {
            funnitureSeletWindow.transform.
                GetComponent<RectTransform>().DOMoveY(-240, 1f);
            gm.TileGrid.SetActive(false);
            funnitureWindow = false;
        }
    }
    public void TileNum(int num)
    {
        GameManager.Instance.tileNum = num;
    }
    public void TileType(int num)
    {
        GameManager.Instance.oType = ObjType.Non;
        GameManager.Instance.oType = (ObjType)num;
    }
}
