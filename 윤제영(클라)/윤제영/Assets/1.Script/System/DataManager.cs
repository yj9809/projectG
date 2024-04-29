using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjData
{
    public GameObject tWParent;
    public GameObject tableParent;
    public GameObject chairParent;
}
public class DataManager : Singleton<DataManager>
{
    public ObjData now = new ObjData();
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/Save";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Save()
    {
        string data = JsonUtility.ToJson(now);
        File.WriteAllText(path, data);
    }
}
