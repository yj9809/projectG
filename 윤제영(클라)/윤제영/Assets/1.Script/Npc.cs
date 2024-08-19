using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    private Spawn spawn;
    private NavMeshAgent nm;
    private Animator ani;
    private GameManager gm;
    private ElementalNpc elementalNpc;

    public Transform target;

    public bool move = true;
    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        spawn = FindObjectOfType<Spawn>();
        ani = transform.GetComponent<Animator>();
        gm = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            nm.SetDestination(target.position);
        }
        if (target.gameObject.name != "End")
        {
            OnSit();
        }
    }
    private void OnSit()
    {
        if (nm.velocity.sqrMagnitude >= 0.2f * 0.2f && nm.remainingDistance <= 0.5f && !ani.GetBool("SitChair"))
        {
            Vector3 ChairPos = 
                new Vector3(target.GetChild(0).transform.position.x, 1.66f, target.GetChild(0).transform.position.z);
            nm.enabled = false;
            move = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, target.GetComponent<Chair>().Check(), 0));
            transform.position = ChairPos;
            ani.SetBool("SitChair", true);
            StartCoroutine(NpcCall());
        }
    }
    // npc 주문 로직 시작
    IEnumerator NpcCall()
    {
        yield return new WaitForSeconds(3f);

        ani.SetBool("Call", true);
        spawn.orderTarget.Enqueue(transform);
    }
    public void Order()
    {
        ani.SetBool("Call", false);
    }
    public void Eat(GameObject food)
    {
        int happyPoint = 0;
        switch (food.name)
        {
            case "Soup":
                happyPoint = 5;
                break;
            case "Tomato Soup":
                happyPoint = 10;
                break;
            case "Potato":
                happyPoint = 20;
                break;
            case "Wheat Bread":
                happyPoint = 30;
                break;
            case "PanCake":
                happyPoint = 50;
                break;
        }

        foodPrefab = Instantiate(food, transform.GetChild(0));
        gm.AllHappy += happyPoint;
        gm.Happy += happyPoint;
        gm.Ui.happyUp += happyPoint;
        ani.SetTrigger("Eat");
        Invoke("TargetChange", 10f);
    }
    // npc 주문 로직 끝
    private void TargetChange()
    {
        Destroy(foodPrefab);
        nm.enabled = true;
        move = true;
        ani.SetBool("SitChair", false);
        spawn.ResetTarget(target);
        int ran = Random.Range(0, spawn.end.Length);
        target = spawn.end[ran];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "End")
        {
            gm.Spawn.CheckCustomer(this.gameObject);
            spawn.pool.ReturnNpc(this.gameObject);
        }
    }
}
