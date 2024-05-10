using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Selet : MonoBehaviour
{
    [SerializeField] private TMP_Text[] slotText;
    [SerializeField] private GameObject creat;

    bool[] saveFile = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))
            {
                saveFile[i] = true;
                ObjData od = DataManager.Instance.LoadData(i);
                slotText[i].text = od.time;
            }
            else
            {
                slotText[i].text = "New File";
            }
        }
    }
    public void SaveCheck(int num)
    {
        DataManager.Instance.nowSlot = num;

        if (saveFile[num])
            GoGame();
        else
            Creat();
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
