using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private  GameObject chair;
    private Vector3 chairPos;
    // Update is called once per frame
    void Update()
    {
        chair = transform.gameObject;
        chairPos = new Vector3(chair.transform.position.x, chair.transform.position.y + 0.2f, chair.transform.position.z);
    }
    public int Check()
    {
        int rotaY = 200;
        if (Physics.Raycast(chairPos, Vector3.forward, 1f, LayerMask.GetMask("Table")))
            rotaY = 0;
        else if (Physics.Raycast(chairPos, Vector3.back, 1f, LayerMask.GetMask("Table")))
            rotaY = 180;

        return rotaY;
    }
}
