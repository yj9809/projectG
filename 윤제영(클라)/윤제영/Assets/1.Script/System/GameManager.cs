using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject tileParent;
    private GameObject tileGrid;
    public GameObject TileGrid
    {
        get
        {
            if (tileGrid == null)
            {
                tileGrid = GameObject.Find("Tile Grid");
            }
            return tileGrid;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tileParent = GameObject.Find("Tile Parent");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
