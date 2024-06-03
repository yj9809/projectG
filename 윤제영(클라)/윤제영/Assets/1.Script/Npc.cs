using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    private Spawn spawn;
    private NavMeshAgent nm;
    private Animator ani;

    public Transform target;

    public bool move = true;
    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        spawn = FindObjectOfType<Spawn>();
        ani = transform.GetComponent<Animator>();
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
        OnSit();
    }
    private void OnSit()
    {
        if (nm.velocity.sqrMagnitude >= 0.2f * 0.2f && nm.remainingDistance <= 0.5f && !ani.GetBool("SitChair"))
        {
            //spawn.SetElementalTaget(transform);
            Vector3 ChairPos = 
            new Vector3(target.GetChild(0).transform.position.x, 1.66f, target.GetChild(0).transform.position.z);
            nm.enabled = false;
            move = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, target.GetComponent<Chair>().Check(), 0));
            transform.position = ChairPos;
            ani.SetBool("SitChair", true);
            StartCoroutine(NpcOrder());
        }
    }
    IEnumerator NpcOrder()
    {
        yield return new WaitForSeconds(5f);

        ani.SetBool("Order", true);
        spawn.OrderTarget.Enqueue(transform);
    }
    IEnumerator TargetChange(float time)
    {
        yield return new WaitForSeconds(time);
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
            spawn.pool.ReturnNpc(this.gameObject);
        }
    }
}
