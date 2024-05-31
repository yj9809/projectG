using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private GameManager gm;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

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
    }
    private void ObjDestroy()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Tile" || hit.transform.gameObject.tag == "Wall" || 
            hit.transform.gameObject.tag == "Table" ||
            hit.transform.gameObject.tag == "Chair")
            {
                Debug.Log(hit.transform.gameObject.name);
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(hit.transform.gameObject);
                    gm.nms.BuildNavMesh();
                }
            }
        }
    }
}
