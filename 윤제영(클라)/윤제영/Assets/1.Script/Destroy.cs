using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private GameManager gm;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private List<Outline> outLine;
    [SerializeField] private List<GameObject> seletObj;

    [SerializeField] private Color originalColor;
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
        ObjDestroy();
        DestroyExecutionButton();
    }
    private void ObjDestroy()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Tile" || 
                hit.transform.gameObject.tag == "Wall" || 
                hit.transform.gameObject.tag == "Table" ||
                hit.transform.gameObject.tag == "Chair" ||
                hit.transform.gameObject.tag == "Counte")
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (GameObject obj in seletObj)
                        if (obj.transform.position == hit.transform.position)
                            return;

                    seletObj.Add(hit.transform.gameObject);
                    outLine.Add(hit.transform.gameObject.AddComponent<Outline>());
                    outLine[outLine.Count - 1].OutlineWidth = 6f;
                    outLine[outLine.Count - 1].OutlineColor = Color.green;
                    gm.navMeshSurface.BuildNavMesh();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearList();
        }
    }
    public void ClearList()
    {
        foreach (Outline outLine in outLine)
        {
            Destroy(outLine);
        }
        outLine.Clear();
        seletObj.Clear();
        DestroyExecutionButton();
    }
    private void DestroyExecutionButton()
    {
        if (outLine.Count > 0)
        {
            gm.Ui.destroyExecution.transform.gameObject.SetActive(true);
            gm.Ui.destroyExecutionEa.text = $"ªË¡¶ ({outLine.Count})";
        }
        else
            gm.Ui.destroyExecution.transform.gameObject.SetActive(false);
    }
    public void DestroyExecution()
    {
        foreach (GameObject obj in seletObj)
        {
            Destroy(obj);
        }
        ClearList();
        gm.navMeshSurface.BuildNavMesh();
    }
}
