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
}
public class DataManager : Singleton<DataManager>
{
    public ObjData now = new ObjData();
    public string path;
    public string fileName = "SaveFile";
    public string filePath;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        path = Application.persistentDataPath + "/Save";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        filePath = Path.Combine(path, fileName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(now);
        File.WriteAllText(filePath, data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(filePath);
        now = JsonUtility.FromJson<ObjData>(data);
    }
    public bool CheckFile()
    {
        return File.Exists(filePath);
    }
}
