using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ObjData
{
    public string saveName = "aaa";
    public int day = 1;
    public int tileGridStep = 0;
    public int allHappyPoint = 0;
    public int happy = 0;
    public int counteEa = 0;
    public int wheatEa = 0;
    public int potatoEa = 0;
    public int tomatoEa = 0;
    public int butterMushroomEa = 0;
    public int timer = 0;
    public List<string> twName = new List<string>();
    public List<Vector3> twPosition = new List<Vector3>();
    public List<Quaternion> twRotation = new List<Quaternion>();
    public List<string> tableName = new List<string>();
    public List<Vector3> tablePosition = new List<Vector3>();
    public List<Quaternion> tableRotation = new List<Quaternion>();
    public List<string> chairName = new List<string>();
    public List<Vector3> chairPosition = new List<Vector3>();
    public List<Quaternion> chairRotation = new List<Quaternion>();
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
        Debug.Log(path);
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
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        string encode = System.Convert.ToBase64String(bytes);
        File.WriteAllText(filePath, encode);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(filePath);
        byte[] bytes = System.Convert.FromBase64String(data);
        string decode = System.Text.Encoding.UTF8.GetString(bytes);
        now = JsonUtility.FromJson<ObjData>(decode);
    }
    public bool CheckFile()
    {
        return File.Exists(filePath);
    }
}
