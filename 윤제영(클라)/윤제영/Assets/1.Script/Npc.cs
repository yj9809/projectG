using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    private Spawn spawn;
    public Transform target;
    NavMeshAgent nm;

    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        spawn = FindObjectOfType<Spawn>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TargetChange(20f));
    }
    // Update is called once per frame
    void Update()
    {
        nm.SetDestination(target.position);
    }
    IEnumerator TargetChange(float time)
    {
        yield return new WaitForSeconds(time);
        spawn.ResetTarget(target);
        int ran = Random.Range(0, spawn.end.Length);
        target = spawn.end[ran];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "End")
        {
            Destroy(transform.gameObject);
        }
    }
}
