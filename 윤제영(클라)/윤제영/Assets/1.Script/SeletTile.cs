using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletTile : MonoBehaviour
{
    [SerializeField] private GameObject[] tile;
    [SerializeField] private GameObject[] wall;

    private Camera mainCamera;
    private GameObject tilePreView;
    private GameManager gm;

    private void Awake()
    {
        mainCamera = Camera.main;
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int wallLayerMask = LayerMask.GetMask("Wall");
        int tileLayerMask = LayerMask.GetMask("Tile");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayerMask | tileLayerMask))
        {
            if (gm.oType == ObjType.Tile && hit.transform.CompareTag("Tile"))
            {
                if (tilePreView == null)
                {
                    tilePreView = Instantiate(tile[gm.tileNum], hit.transform.position, Quaternion.identity);
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
    public void SetTile(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name != tile[gm.tileNum].name)
            {
                GameObject newTile = Instantiate(tile[gm.tileNum], position, Quaternion.identity);
                newTile.name = tile[gm.tileNum].name;
                newTile.transform.SetParent(gm.tileParent.transform);
            }
            else if(hit.transform.gameObject.name == tile[gm.tileNum].name)
            {
                Destroy(hit.transform.gameObject);
                GameObject newTile = Instantiate(tile[gm.tileNum], position, Quaternion.identity);
                newTile.name = tile[gm.tileNum].name;
                newTile.transform.SetParent(gm.tileParent.transform);
            }
        } 
    }
    public void SetWall(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
        {
            if (hit.transform.gameObject.name != wall[gm.tileNum].name)
            {
                GameObject newWall = Instantiate(wall[gm.tileNum], position, tilePreView.transform.rotation);
                newWall.name = wall[gm.tileNum].name;
                newWall.transform.SetParent(gm.tileParent.transform);
            }
        }
    }
}


