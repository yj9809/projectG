using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjType
{
    Tile,
    Wall,
    Non
}
public class GameManager : Singleton<GameManager>
{
    public int tileNum;
    public ObjType oType = ObjType.Non;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
