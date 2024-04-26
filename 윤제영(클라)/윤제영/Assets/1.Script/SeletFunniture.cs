using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletFunniture : MonoBehaviour
{
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject chair;
    Ray ray;
    RaycastHit hit;
    private GameManager gm;
    private Camera mainCamera;
    private Camera mc;

    public GameObject funniturePreView;
    bool set = true;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (gm.fType == FunnitureType.Table)
            TableSelet();
        else if (gm.fType == FunnitureType.Chair)
            ChairSelet();

    }
    // 테이블 세팅
    private void TableSelet()
    {
        int tileLayer = LayerMask.GetMask("Tile");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            if (funniturePreView == null)
            {
                funniturePreView = Instantiate(table, hit.transform.position, Quaternion.identity);
                funniturePreView.transform.GetComponent<Collider>().enabled = false;
            }
            else
            {
                funniturePreView.transform.position = hit.transform.position;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 tableRotation = funniturePreView.transform.rotation.eulerAngles;
                SetTable(hit.transform.position, tableRotation);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 tableRotation = funniturePreView.transform.rotation.eulerAngles;
                tableRotation.y += 90f;
                funniturePreView.transform.rotation = Quaternion.Euler(tableRotation);
            }
        }
        else
        {
            if (funniturePreView != null)
            {
                Destroy(funniturePreView);
            }
        }
    }
    private void SetTable(Vector3 pos, Vector3 rota)
    {
        int tileLayer = LayerMask.GetMask("Tile");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            foreach (Transform child in gm.tableParent.transform)
            {
                if (child.transform.position == hit.transform.position)
                    return;
            }
            GameObject newTable = Instantiate(table, pos, Quaternion.Euler(rota));
            newTable.name = table.name;
            newTable.transform.SetParent(gm.tableParent.transform);
        }
    }
    // 의자 세팅
    private void ChairSelet()
    {
        int interiorLayer = LayerMask.GetMask("Interior Grid");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interiorLayer))
        {
            if (funniturePreView == null)
            {
                Vector3 pos =
                    new Vector3(hit.transform.position.x + 0.5425f, hit.transform.position.y, hit.transform.position.z);
                funniturePreView = Instantiate(chair, pos, Quaternion.identity);
                funniturePreView.transform.GetComponent<Collider>().enabled = false;
            }
            else
            {
                funniturePreView.transform.position = 
                    new Vector3(hit.transform.position.x + 0.5425f, hit.transform.position.y, hit.transform.position.z);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos =
                    new Vector3(hit.transform.position.x + 0.5425f, hit.transform.position.y, hit.transform.position.z);
                Instantiate(chair, pos, Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 chairRotation = funniturePreView.transform.rotation.eulerAngles;
                chairRotation.y += 90f;
                funniturePreView.transform.rotation = Quaternion.Euler(chairRotation);
            }
        }
    }
}
