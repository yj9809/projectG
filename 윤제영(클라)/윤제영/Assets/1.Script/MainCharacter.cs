using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacter : MonoBehaviour
{
    private GameManager gm;
    private NavMeshAgent nm;
    private Animator ani;

    public Transform target;

    public bool isMove = false;
    public bool goHome = false;
    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            nm.SetDestination(target.position);
    }
    public void GoHouse()
    {
        ani.SetBool("Move", true);
        target = gm.House.housePos;
        goHome = true;
        isMove = true;
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
