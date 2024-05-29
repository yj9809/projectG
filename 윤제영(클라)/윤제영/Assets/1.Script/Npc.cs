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
        //StartCoroutine(TargetChange(20f));
    }
    // Update is called once per frame
    void Update()
    {
        if (move)
            nm.SetDestination(target.position);
        if (nm.velocity.sqrMagnitude >= 0.2f * 0.2f && nm.remainingDistance <= 0.5f && !ani.GetBool("SitChair"))
        {
            //spawn.SetElementalTaget(transform);
            move = false;
            transform.position = 
                new Vector3(target.GetChild(0).transform.position.x, target.GetChild(0).transform.position.y + 1, target.GetChild(0).transform.position.z);
            transform.rotation = Quaternion.Euler(new Vector3(0, target.GetComponent<Chair>().Check(), 0));
            ani.SetBool("SitChair", true);
        }
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
