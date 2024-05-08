using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Selet : MonoBehaviour
{
    [SerializeField] private TMP_Text[] slotText;

    bool[] saveFile = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))
            {
                Debug.Log("if");
                saveFile[i] = true;
                ObjData od = DataManager.Instance.LoadData(i);
                slotText[i].text = od.time;
            }
            else
            {
                Debug.Log("else");
                slotText[i].text = "New File" + $"{i}";
            }
        }
    }
    public void GoGame()
    {
        DataManager.Instance.LoadData();
        GameManager.Instance.OnLoadScene();
    }
}
