using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ui : MonoBehaviour
{
    [SerializeField] private Image tileSeletWindow;

    private bool tileWindow;
    private void Start()
    {
        tileWindow = false;
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
                GetComponent<RectTransform>().DOMoveY(tileSeletWindow.transform.position.y + 240 , 1f);

            tileWindow = true;
        }
        else if (tileWindow)
        {
            tileSeletWindow.transform.
                GetComponent<RectTransform>().DOMoveY(tileSeletWindow.transform.position.y - 240, 1f);

            tileWindow = false;
        }
    }
    public void TileNum(int num)
    {
        GameManager.Instance.tileNum = num;
    }
}
