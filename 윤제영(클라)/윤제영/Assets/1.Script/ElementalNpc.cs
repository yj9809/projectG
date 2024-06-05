using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElementalNpc : MonoBehaviour
{
    [SerializeField] private GameObject food;

    private Spawn spawn;
    private NavMeshAgent nm;
    private Transform orderTarget;

    public Transform target;
    
    private float changeTargetTimer = 1;
    private float changeTargetTime = float.MaxValue;

    public bool setTarget = false;
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
        if (!setTarget)
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
            setTarget = true;
            orderTarget = spawn.OrderTarget.Dequeue();
            target = orderTarget;
            StartCoroutine(GoCounte()); 
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
    private void GoServing(GameObject food)
    {
        target = orderTarget;
        if (nm.velocity.sqrMagnitude >= 0.2f * 0.2f && nm.remainingDistance <= 0.5f)
        {
            Debug.Log("½ÇÇà");
            Destroy(food);
            RandomTarget();
        }
    }
    IEnumerator GoCounte()
    {
        yield return new WaitForSeconds(3f);

        target = GameManager.Instance.CountePos;

        yield return new WaitForSeconds(3f);

        GameObject foodPrefab = Instantiate(food, transform.GetChild(0).transform);
        GoServing(foodPrefab);
    }
}
