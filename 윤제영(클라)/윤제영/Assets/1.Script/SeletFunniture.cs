    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class SeletFunniture : MonoBehaviour
{
    [SerializeField] private GameObject[] table;
    [SerializeField] private GameObject[] chair;
    [SerializeField] private Spawn spawn;

    public GameObject funniturePreView;

    private GameManager gm;
    private GameObject check;
    private GameObject chairCheck;
    private Camera mainCamera;

    Ray ray;
    RaycastHit hit;
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
    // ﾅﾗﾀﾌｺ・ｼｼﾆﾃ
    private void TableSelet()
    {
        int tileLayer = LayerMask.GetMask("Tile");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            if (funniturePreView == null)
            {
                funniturePreView = Instantiate(table[gm.funnitureNum], hit.transform.position, Quaternion.identity);
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            foreach (Transform child in gm.TableParent.transform)
            {
                if (child.transform.position == hit.transform.position)
                    return;
            }
            GameObject newTable = Instantiate(table[gm.funnitureNum], pos, Quaternion.Euler(rota));
            newTable.name = table[gm.funnitureNum].name;
            newTable.transform.SetParent(gm.TableParent);
            gm.nms.BuildNavMesh();
        }
    }
    // ﾀﾇﾀﾚ ｼｼﾆﾃ
    private void ChairSelet()
    {
        int interiorLayer = LayerMask.GetMask("Interior Grid");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interiorLayer))
        {
            if (funniturePreView == null)
            {
                funniturePreView = Instantiate(chair[gm.funnitureNum], hit.transform.position, Quaternion.identity);
                funniturePreView.transform.GetComponent<Collider>().enabled = false;
            }
            else
                funniturePreView.transform.position = hit.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 chairRotation = funniturePreView.transform.rotation.eulerAngles;
                SteChair(hit.transform.position, chairRotation);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 chairRotation = funniturePreView.transform.rotation.eulerAngles;
                if (chairRotation.y == 270)
                    chairRotation.y = 0;
                else
                    chairRotation.y += 90f;
                funniturePreView.transform.rotation = Quaternion.Euler(chairRotation);
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
    private void SteChair(Vector3 pos, Vector3 rota)
    {
        check = funniturePreView.transform.gameObject;
        Vector3 checkPos = new Vector3(check.transform.position.x, check.transform.position.y + 0.2f, check.transform.position.z);
        RaycastHit hitCheck;
        bool isHit = Physics.Raycast(checkPos, Vector3.down, out hitCheck, 1f, LayerMask.GetMask("Interior Grid"));
        Debug.Log(isHit);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && isHit)
        {
            if (hit.transform.CompareTag("Chair"))
            {
                return;
            }  
            else if (!hit.transform.CompareTag("Chair"))    
            {
                GameObject newChair = Instantiate(chair[gm.funnitureNum], pos, Quaternion.Euler(rota));
                newChair.transform.SetParent(gm.ChairParent);
                newChair.name = chair[gm.funnitureNum].name;

                gm.nms.BuildNavMesh();
                spawn.npcTarget.Add(newChair.transform);
                spawn.SetRandomTarget();
            }
        }
    }
    private bool ChairOverLapCheck(Vector3 rota)
    {
        chairCheck = funniturePreView.transform.GetChild(0).GetChild(1).gameObject;

        RaycastHit hitResult;

        if (rota.y == 0)
            Physics.Raycast(chairCheck.transform.position, Vector3.right, out hitResult, 1f, LayerMask.GetMask("Chair"));
        else if (rota.y == 90)
            Physics.Raycast(chairCheck.transform.position, Vector3.back, out hitResult, 1f, LayerMask.GetMask("Chair"));
        else if (rota.y == 180)
            Physics.Raycast(chairCheck.transform.position, Vector3.left, out hitResult, 1f, LayerMask.GetMask("Chair"));
        else
            Physics.Raycast(chairCheck.transform.position, Vector3.forward, out hitResult, 1f, LayerMask.GetMask("Chair"));

        return !hitResult.collider;
    }
}
