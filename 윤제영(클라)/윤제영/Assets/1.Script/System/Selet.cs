using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Selet : MonoBehaviour
{
    [SerializeField] private GameObject load;
    [SerializeField] private GameObject creat;

    // Start is called before the first frame update
    void Start()
    {
        SaveCheck();
    }
    public void SaveCheck()
    {
        Debug.Log(DataManager.Instance.CheckFile());
        if (DataManager.Instance.CheckFile())
        {
            load.SetActive(true);
        }
        else
        {
            load.SetActive(false);
        }
    }
    public void GoGame()
    {
        DataManager.Instance.LoadData();
        GameManager.Instance.OnLoadScene();
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
}
