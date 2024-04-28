using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletTile : MonoBehaviour
{
    [SerializeField] private GameObject[] tile;
    [SerializeField] private GameObject[] wall;
    [SerializeField] private Material[] wallMate;
    [SerializeField] private Material[] tileMate;

    private GameManager gm;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;
    private GameObject tilePreView;

    private void Awake()
    {
        gm = GameManager.Instance;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        TileGirdSelet();
    }
    private void TileGirdSelet()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (gm.oType == TileType.Tile && hit.transform.CompareTag("Tile"))
            {
                if (tilePreView == null)
                {
                    tilePreView = Instantiate(tile[gm.tileNum], hit.transform.position, Quaternion.identity);
                    tilePreView.transform.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    tilePreView.transform.position = hit.transform.position;
                }
                if (Input.GetMouseButton(0))
                {
                    SetTile(hit.transform.position);
                }
            }
            else if (gm.oType == TileType.Wall && hit.transform.CompareTag("Wall"))
            {
                if (tilePreView == null)
                {
                    tilePreView = Instantiate(wall[gm.tileNum], hit.transform.position, hit.transform.rotation);
                    tilePreView.transform.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    tilePreView.transform.position = hit.transform.position;
                    tilePreView.transform.rotation = hit.transform.rotation;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SetWall(hit.transform.position);
                }
            }
            else
            {
                if (tilePreView != null)
                {
                    Destroy(tilePreView);
                }
            }
        }
        else
        {
            if (tilePreView != null)
            {
                Destroy(tilePreView);
            }
        }
    }
    //鸥老 积己
    public void SetTile(Vector3 position)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == tile[gm.tileNum].name)
                return;
            else if (hit.transform.gameObject.name != tile[gm.tileNum].name)
            {
                GameObject newTile;

                if (hit.transform.gameObject.layer == 6)
                    newTile = Instantiate(tile[gm.tileNum], position, Quaternion.identity);
                else if (hit.transform.CompareTag("Wall"))
                    return;
                else
                {
                    newTile = hit.transform.gameObject;
                    Material[] mate = newTile.transform.GetComponent<MeshRenderer>().materials;
                    mate[0] = tileMate[gm.tileNum];
                    newTile.transform.GetComponent<MeshRenderer>().materials = mate;
                }
                newTile.name = tile[gm.tileNum].name;
                newTile.transform.SetParent(gm.tWParent);
            }
            
        } 
    }
    //寒 积己
    public void SetWall(Vector3 position)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
        {
            if (hit.transform.gameObject.name == wall[gm.tileNum].name)
                return;
            else if (hit.transform.gameObject.name != wall[gm.tileNum].name) 
            {
                GameObject newWall;

                if (hit.transform.gameObject.layer == 6)
                    newWall = Instantiate(wall[gm.tileNum], position, tilePreView.transform.rotation);
                else if (hit.transform.CompareTag("Tile"))
                    newWall = Instantiate(wall[gm.tileNum], position, tilePreView.transform.rotation);
                else
                {
                    newWall = hit.transform.gameObject;
                    Material[] mate = newWall.transform.GetComponent<MeshRenderer>().materials;
                    mate[0] = wallMate[gm.tileNum];
                    newWall.transform.GetComponent<MeshRenderer>().materials = mate;
                }

                newWall.name = wall[gm.tileNum].name;
                newWall.transform.SetParent(gm.tWParent);
            }
        }
    }
}


