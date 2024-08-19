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
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Stop)
            return;
        // 서빙을 하기 위한 enum 타입에 맞춰 switch 문을 통한 행동 결정
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
        }
        if (isMove)
            nm.SetDestination(target.position);
    }
    private void RandomTarget()
    {
        if (spawn.orderTarget.Count > 0)
        {
            orderTarget = spawn.orderTarget.Dequeue();
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
    // 정령들이 서빙을 위한 로직 함수 시작
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
            if (gm.foods.Count <= 0)
            {
                foodPrefab = Instantiate(food, transform.GetChild(0).transform);
                foodPrefab.name = food.name;
            }
            else
            {
                GameObject food = gm.foods.Dequeue();
                foodPrefab = Instantiate(food, transform.GetChild(0).transform);
                foodPrefab.name = food.name;
            }

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
    // 정령들이 서빙을 위한 로직 함수 끝
    public void GoHouse()
    {
        goHome = true;
        isMove = true;
        isRandom = false;
        target = gm.House.housePos;
    }
    public void GoStore()
    {
        transform.position = gm.House.housePos.position;
        nm.enabled = true;
        RandomTarget();
        goHome = false;
        isMove = true;
        isRandom = true;
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
