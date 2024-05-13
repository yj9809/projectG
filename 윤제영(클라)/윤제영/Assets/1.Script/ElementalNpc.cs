using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElementalNpc : MonoBehaviour
{
    private Spawn spawn;
    private Transform target;
    private NavMeshAgent nm;

    private float changeTargetTimer = 1;
    private float changeTargetTime = float.MaxValue;

    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        spawn = FindObjectOfType<Spawn>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        changeTargetTime += Time.deltaTime;
        if (changeTargetTime >= changeTargetTimer)
        {
            int randomTarget = Random.Range(0, spawn.elementalTarget.Length);
            target = spawn.elementalTarget[randomTarget];

            changeTargetTimer = RandomTime();
            changeTargetTime = 0;
        }

        nm.SetDestination(target.position);
    }
    private int RandomTime()
    {
        int randomTime = Random.Range(3, 6);

        return randomTime;
    }
}
