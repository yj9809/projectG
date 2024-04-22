using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletTile : MonoBehaviour
{
    [SerializeField] private GameObject[] tile;
    [SerializeField] private GameObject[] wall;

    private Camera mainCamera;
    public GameObject tilePreView;
    private GameManager gm;

    private void Awake()
    {
        mainCamera = Camera.main;
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        TileGirdSelet();
    }
    private void TileGirdSelet()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int tileGirdLayerMask = LayerMask.GetMask("Tile Grid");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileGirdLayerMask))
        {
            if (gm.oType == ObjType.Tile && hit.transform.CompareTag("Tile"))
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
            else if (gm.oType == ObjType.Wall && hit.transform.CompareTag("Wall"))
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
    }
    //鸥老 积己
    public void SetTile(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == tile[gm.tileNum].name)
                return;
            else if (hit.transform.gameObject.name != tile[gm.tileNum].name)
            {
                GameObject newTile;
                if (hit.transform.gameObject.layer == 6)
                    newTile = Instantiate(tile[gm.tileNum], position, Quaternion.identity);
                else
                {
                    Destroy(hit.transform.gameObject);
                    newTile = Instantiate(tile[gm.tileNum], position, Quaternion.identity);
                }
                newTile.name = tile[gm.tileNum].name;
                newTile.transform.SetParent(gm.tileParent.transform);
            }
            
        } 
    }
    //寒 积己
    public void SetWall(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
        {
            if (hit.transform.gameObject.name == wall[gm.tileNum].name)
                return;
            else if (hit.transform.gameObject.name != wall[gm.tileNum].name)
            {
                GameObject newWall;
                if (hit.transform.gameObject.layer == 6)
                    newWall = Instantiate(wall[gm.tileNum], position, tilePreView.transform.rotation);
                else
                {
                    Destroy(hit.transform.gameObject);
                    newWall = Instantiate(wall[gm.tileNum], position, tilePreView.transform.rotation);
                }
                newWall.name = wall[gm.tileNum].name;
                newWall.transform.SetParent(gm.tileParent.transform);
            }
        }
    }
}


