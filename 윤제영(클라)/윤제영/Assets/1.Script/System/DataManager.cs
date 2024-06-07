using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ObjData
{
    public string saveName = "aaa";
    public string time;
    public int day;
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
    public void SavePrefab(GameObject temp)
    {
        string fileName = "Save Obj Prefab";
        string path = "Assets/Resources/Test/" + fileName + ".prefab";

        bool isSuccess = false;
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(temp, path, out isSuccess);// ¿˙¿Â
        Debug.Log(isSuccess);
        //UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
    }
}
