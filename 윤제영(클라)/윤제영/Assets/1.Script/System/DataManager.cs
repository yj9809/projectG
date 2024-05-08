using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ObjData
{
    public string saveName;
    public string time;
    public GameObject tWParent;
    public GameObject tableParent;
    public GameObject chairParent;
}
public class DataManager : Singleton<DataManager>
{
    public ObjData now = new ObjData();
    public string path;
    public int nowSlot;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/Save";
        nowSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(now);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        now = JsonUtility.FromJson<ObjData>(data);
    }

    public ObjData LoadData(int index)
    {
        string data = File.ReadAllText(path + index.ToString());
        ObjData od = JsonUtility.FromJson<ObjData>(data);

        return od;
    }
}
