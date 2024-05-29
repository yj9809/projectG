using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private  GameObject chair;
    // Update is called once per frame
    void Update()
    {
        chair = transform.GetChild(0).gameObject;
        Debug.DrawRay(chair.transform.position, Vector3.forward, Color.red);
        Debug.Log(Check());
    }
    public int Check()
    {
        int rotaY = 200;
        if (Physics.Raycast(chair.transform.position, Vector3.forward, 1f, LayerMask.GetMask("Table")))
            rotaY = 180;
        else if (Physics.Raycast(chair.transform.position, Vector3.back, 1f, LayerMask.GetMask("Table")))
            rotaY = 0;

        return rotaY;
    }
}
