using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ServingType
{
    Idle,
    GoCounte,
    GoServing
}
public class ElementalNpc : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform orderTarget;

    private Spawn spawn;
    private NavMeshAgent nm;

    public Transform target;

    public ServingType sType = ServingType.Idle;
    
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
        Debug.Log($"{this.gameObject.name} + {nm.remainingDistance}");
        if (sType == ServingType.Idle)
        {
            changeTargetTime += Time.deltaTime;

            if (changeTargetTime >= changeTargetTimer)
            {
                RandomTarget();
            }
        }
        else if(sType == ServingType.GoCounte)
        {
            GoCounte();
        }
        else if (sType == ServingType.GoServing)
        {
            GoServing();
        }
        nm.SetDestination(target.position);
    }
    private void RandomTarget()
    {
        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            if (spawn.OrderTarget.Count > 0)
            {
                orderTarget = spawn.OrderTarget.Dequeue();
                target = orderTarget;

                if (nm.remainingDistance <= 0.5f)
                {
                    sType = ServingType.GoCounte;
                }
            }
            else
            {
                Debug.Log("실행 2");
                int randomTarget = Random.Range(0, spawn.elementalTarget.Length);
                target = spawn.elementalTarget[randomTarget];

                changeTargetTime = 0;
                changeTargetTimer = RandomTime();
            }
        }
    }
    private int RandomTime()
    {
        int randomTime = Random.Range(3, 6);

        return randomTime;
    }
    private void GoCounte()
    {
        target = GameManager.Instance.CounterPos;

        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            sType = ServingType.GoServing;
            foodPrefab = Instantiate(food, transform.GetChild(0).transform);
        }
    }
    private void GoServing()
    {
        target = orderTarget;

        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            Debug.Log($"실행 +{this.gameObject.name}  ");
            //Destroy(food);
            sType = ServingType.Idle;
        }
    }
}
