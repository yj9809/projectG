using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletTile : MonoBehaviour
{
    [SerializeField] private GameObject[] tile;
    [SerializeField] private GameObject[] wall;
    [SerializeField] private GameObject[] counter;
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
            if (hit.transform.CompareTag("Tile"))
            {
                if (gm.oType == TileType.Tile)
                {
                    if (tilePreView == null)
                    {
                        tilePreView = Instantiate(tile[gm.TileNum], hit.transform.position, Quaternion.identity);
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
                else if(gm.oType == TileType.Counter)
                {
                    Vector3 pos = new Vector3(hit.transform.position.x + 1f, hit.transform.position.y + 0.01f, hit.transform.position.z);
                    if (tilePreView == null)
                    {
                        tilePreView = Instantiate(counter[gm.TileNum], pos, Quaternion.identity);
                        tilePreView.transform.GetComponent<Collider>().enabled = false;
                        tilePreView.transform.GetChild(9).GetChild(1).GetComponent<Collider>().enabled = false;
                    }
                    else
                    {
                        tilePreView.transform.position = pos;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        SetCounter(pos);
                    }
                }
            }
            else if (gm.oType == TileType.Wall && hit.transform.CompareTag("Wall"))
            {
                Vector3 pos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1.25f, hit.transform.position.z);
                if (tilePreView == null)
                {
                    tilePreView = Instantiate(wall[gm.TileNum], pos, hit.transform.rotation);
                    tilePreView.transform.GetComponent<Collider>().enabled = false;
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
    private void SetCounter(Vector3 position)
    {
        if (!tilePreView.transform.GetComponent<Counter>().TileCheck())
            return;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity) && gm.CounteEa == 0)
        {
            if (hit.transform.gameObject.name == counter[gm.TileNum].name)
                return;
            else if(hit.transform.gameObject.name != counter[gm.TileNum].name)
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    GameObject newCounter = Instantiate(counter[gm.TileNum], position, Quaternion.identity);
                    newCounter.name = counter[gm.TileNum].name;
                    newCounter.transform.SetParent(gm.TWParent);
                    gm.CounteEa += 1;
                    gm.navMeshSurface.BuildNavMesh();
                }
                else if (hit.transform.CompareTag("Wall") && hit.transform.CompareTag("Tile"))
                    return;
            }
        }
    }
    //鸥老 积己
    public void SetTile(Vector3 position)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == tile[gm.TileNum].name)
                return;
            else if (hit.transform.gameObject.name != tile[gm.TileNum].name)
            {
                GameObject newTile;

                if (hit.transform.gameObject.layer == 6)
                    newTile = Instantiate(tile[gm.TileNum], position, Quaternion.identity);
                else if (hit.transform.CompareTag("Wall") && hit.transform.CompareTag("Counte"))
                    return;
                else
                {
                    newTile = hit.transform.gameObject;
                    Material[] mate = newTile.transform.GetComponent<MeshRenderer>().materials;
                    mate[0] = tileMate[gm.TileNum];
                    newTile.transform.GetComponent<MeshRenderer>().materials = mate;
                }
                newTile.name = tile[gm.TileNum].name;
                newTile.transform.SetParent(gm.TWParent);
                gm.navMeshSurface.BuildNavMesh();
            }
        } 
    }
    //寒 积己
    public void SetWall(Vector3 position)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
        {
            if (hit.transform.gameObject.name == wall[gm.TileNum].name)
                return;
            else if (hit.transform.gameObject.name != wall[gm.TileNum].name) 
            {
                GameObject newWall;

                Vector3 pos = new Vector3(position.x, position.y + 1.25f, position.z);

                if (hit.transform.gameObject.layer == 6)
                    newWall = Instantiate(wall[gm.TileNum], pos, tilePreView.transform.rotation);
                else if (hit.transform.CompareTag("Tile") && hit.transform.CompareTag("Counte"))
                    newWall = Instantiate(wall[gm.TileNum], pos, tilePreView.transform.rotation);
                else
                {
                    newWall = hit.transform.gameObject;
                    Material[] mate = newWall.transform.GetComponent<MeshRenderer>().materials;
                    mate[0] = wallMate[gm.TileNum];
                    newWall.transform.GetComponent<MeshRenderer>().materials = mate;
                }

                newWall.name = wall[gm.TileNum].name;
                newWall.transform.SetParent(gm.TWParent);
                gm.navMeshSurface.BuildNavMesh();
            }
        }
    }
}


