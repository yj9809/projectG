using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ServingType
{
    Idle,
    GoGuest,
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
    private GameManager gm;

    public Transform target;

    public ServingType sType = ServingType.Idle;
    
    private float changeTargetTimer = 1;
    private float changeTargetTime = float.MaxValue;

    public bool isRandom = true;
    public bool isMove = true;
    public bool goHome = false;
    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        spawn = FindObjectOfType<Spawn>();
        gm = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamestate == GameState.Stop)
            return;

        switch (sType)
        {
            case ServingType.Idle:
                if (isRandom)
                {
                    changeTargetTime += Time.deltaTime;

                    if (changeTargetTime >= changeTargetTimer)
                    {
                        RandomTarget();
                    }
                }
                break;
            case ServingType.GoGuest:
                GoGuest();
                break;
            case ServingType.GoCounte:
                GoCounte();
                break;
            case ServingType.GoServing:
                GoServing(foodPrefab);
                break;
            default:
                break;
        }
        if (isMove)
            nm.SetDestination(target.position);
    }
    private void RandomTarget()
    {
        if (spawn.OrderTarget.Count > 0)
        {
            orderTarget = spawn.OrderTarget.Dequeue();
            target = orderTarget;
            sType = ServingType.GoGuest;
            isRandom = false;
        }
        else
        {
            int randomTarget = Random.Range(0, spawn.elementalTarget.Length);
            target = spawn.elementalTarget[randomTarget];

            changeTargetTime = 0;
            changeTargetTimer = RandomTime();
        }
    }
    private int RandomTime()
    {
        int randomTime = Random.Range(3, 6);

        return randomTime;
    }
    private void GoGuest()
    {
        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            target.GetComponent<Npc>().Order();
            target = GameManager.Instance.CounterPos;
            sType = ServingType.GoCounte;
        }
    }
    public void GoCounte()
    {
        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            foodPrefab = Instantiate(food, transform.GetChild(0).transform);
            target = orderTarget;
            sType = ServingType.GoServing;
        }
    }
    private void GoServing(GameObject food)
    {
        if (nm.remainingDistance <= nm.stoppingDistance)
        {
            target.GetComponent<Npc>().Eat(food);
            Destroy(food);
            isRandom = true;
            sType = ServingType.Idle;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House" && goHome)
        {
            nm.enabled = false;
            isMove = false;
            transform.position = gm.House.houseInPos.position;
            gm.House.partner.Add(gameObject);
        }
    }
}
