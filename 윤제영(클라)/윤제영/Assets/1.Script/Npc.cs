using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform disColl;
    NavMeshAgent nm;

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(targetChance());
    }

    // Update is called once per frame
    void Update()
    {
        nm.SetDestination(target.position);   
    }
    IEnumerator targetChance()
    {
        yield return new WaitForSeconds(10f);

        target = disColl;
    }
}
