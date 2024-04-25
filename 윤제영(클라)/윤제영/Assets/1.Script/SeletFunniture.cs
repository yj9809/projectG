using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletFunniture : MonoBehaviour
{
    [SerializeField] private GameObject table;

    Ray ray;
    RaycastHit hit;
    private GameManager gm;
    private Camera mainCamera;
    private Camera mc;

    public GameObject tablePreView;
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
        TableSelet();
    }
    private void TableSelet()
    {
        int tileLayer = LayerMask.GetMask("Tile");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            if (tablePreView == null)
            {
                tablePreView = Instantiate(table, hit.transform.position, Quaternion.identity);
                tablePreView.transform.GetComponent<Collider>().enabled = false;
            }
            else
            {
                tablePreView.transform.position = hit.transform.position;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 tableRotation = tablePreView.transform.rotation.eulerAngles;
                SetTable(hit.transform.position, tableRotation);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 tableRotation = tablePreView.transform.rotation.eulerAngles;
                tableRotation.y += 90f;
                tablePreView.transform.rotation = Quaternion.Euler(tableRotation);
            }
        }
        else
        {
            if (tablePreView != null)
            {
                Destroy(tablePreView);
            }
        }
    }
    private void SetTable(Vector3 pos, Vector3 rota)
    {
        GameObject newTable = Instantiate(table, pos, Quaternion.Euler(rota));
        newTable.name = table.name;
        newTable.transform.SetParent(gm.tableParent.transform);
    }
    
}
