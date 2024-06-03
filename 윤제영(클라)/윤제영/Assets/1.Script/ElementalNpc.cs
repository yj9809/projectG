using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElementalNpc : MonoBehaviour
{
    private Spawn spawn;
    private NavMeshAgent nm;

    public Transform target;

    private float changeTargetTimer = 1;
    private float changeTargetTime = float.MaxValue;

    public bool setTaget = false;
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
        if (!setTaget)
        {
            changeTargetTime += Time.deltaTime;

            if (changeTargetTime >= changeTargetTimer)
            {
                RandomTarget();
            }
        }
        nm.SetDestination(target.position);
    }
    private void RandomTarget()
    {
        if (spawn.OrderTarget.Count > 0)
        {
            setTaget = true;
            target = spawn.OrderTarget.Dequeue();
        }
        else
        {
            int randomTarget = Random.Range(0, spawn.elementalTarget.Length);
            target = spawn.elementalTarget[randomTarget];

            changeTargetTimer = RandomTime();
            changeTargetTime = 0;
        }
    }
    private int RandomTime()
    {
        int randomTime = Random.Range(3, 6);

        return randomTime;
    }
}
