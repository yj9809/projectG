using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 endPosition = transform.position + Vector3.right * 1f;
        Debug.DrawRay(transform.position, endPosition - transform.position, Color.red);
        Vector3 endPosition1 = transform.position + Vector3.back * 1f;
        Debug.DrawRay(transform.position, endPosition1 - transform.position, Color.red);
        Vector3 endPosition2 = transform.position + Vector3.left * 1f;
        Debug.DrawRay(transform.position, endPosition2 - transform.position, Color.red);
        Vector3 endPosition3 = transform.position + Vector3.forward * 1f;
        Debug.DrawRay(transform.position, endPosition3 - transform.position, Color.red);
    }
}
