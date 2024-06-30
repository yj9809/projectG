using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Transform[] tileCheck;

    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        
        Debug.DrawRay(tileCheck[0].position, Vector3.down, Color.red);

    }
    public bool TileCheck()
    {
        if (Physics.Raycast(tileCheck[0].position, Vector3.down, out hit, 1f))
        {
            Debug.Log(hit.transform.gameObject.tag);
        }
        for (int i = 0; i < tileCheck.Length; i++)
        {
            if (Physics.Raycast(tileCheck[i].position, Vector3.down, out hit,1f))
            {
                if (hit.transform.gameObject.tag != "Tile")
                {
                    Debug.Log(i);
                    return false;
                }
            }
        }

        return true;
    }
}
