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
    [SerializeField] private TMP_Text creatName;
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
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
    public void NewGame()
    {
        GameManager.Instance.buildingPrefab = Resources.Load<GameObject>("Test/Nomal Obj Prefab");
        DataManager.Instance.now.saveName = creatName.text;
        DataManager.Instance.SaveData();
        GameManager.Instance.OnLoadScene();
    }
    public void LoadGame()
    {
        GameManager.Instance.buildingPrefab = Resources.Load<GameObject>("Test/Save Obj Prefab");
        DataManager.Instance.LoadData();
        GameManager.Instance.OnLoadScene();
    }
}
