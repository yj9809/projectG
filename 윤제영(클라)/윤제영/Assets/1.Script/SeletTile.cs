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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.tag);

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
                if (Input.GetMouseButtonDown(0))
                {
                    SetTile(hit.transform.position);
                }
            }
            else if (gm.oType == ObjType.Wall && hit.transform.CompareTag("Wall"))
            {
                if (tilePreView == null)
                {
                    tilePreView = Instantiate(wall[gm.tileNum], hit.transform.position, Quaternion.identity);
                    tilePreView.transform.rotation = hit.transform.localRotation;
                }
                else
                {
                    tilePreView.transform.position = hit.transform.position;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SetTile(hit.transform.position);
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
        
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    Debug.Log(hit.transform.gameObject.tag);
        //    if (hit.transform.CompareTag("Tile"))
        //    {
        //        if (tilePreView == null)
        //        {
        //            tilePreView = Instantiate(tile[gm.tileNum], hit.transform.position, Quaternion.identity);
        //        }
        //        else
        //        {
        //            tilePreView.transform.position = hit.transform.position;
        //        }
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            SetTile(hit.transform.position);
        //        }
        //    }
        //    else if (hit.transform.CompareTag("Wall"))
        //    {
        //        if (tilePreView == null)
        //        {
        //            tilePreView = Instantiate(wall[0], hit.transform.position, Quaternion.identity);
        //            tilePreView.transform.rotation = hit.transform.rotation;
        //        }
        //        else if (tilePreView.gameObject.tag != "Wall")
        //        {
        //            tilePreView = Instantiate(wall[0], hit.transform.position, Quaternion.identity);
        //            tilePreView.transform.rotation = hit.transform.rotation;
        //        }
        //        else
        //        {
        //            Destroy(tilePreView);
        //        }
        //    }
        //}
        //else
        //{
        //    if (tilePreView != null)
        //    {
        //        Destroy(tilePreView);
        //    }
        //}
    }
    public void SetTile(Vector3 position)
    {
        Instantiate(tile[gm.tileNum], position, Quaternion.identity);
    }
    public void SetWall(Vector3 position)
    {
        //Instantiate(wall, pos, Quaternion.identity);
    }
}


