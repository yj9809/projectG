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
    public bool goCounter = false;
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
        if (gm.gamestate == GameState.Stop)
            return;

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
    public void GoCounter()
    {
        transform.position = gm.House.housePos.position;
        nm.enabled = true;
        ani.SetBool("Move", true);
        target = gm.ProtagonistPos;
        goHome = false;
        goCounter = true;
        isMove = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House" && goHome)
        {
            ani.SetBool("Move", false);
            nm.enabled = false;
            isMove = false;
            transform.position = gm.House.houseInPos.position;
            gm.House.partner.Add(gameObject);
        }
        if (other.gameObject.tag == "Counte" && goCounter)
        {
            ani.SetBool("Move", false);
            isMove = false;
            goCounter = false;
            transform.position = gm.ProtagonistPos.position;
        }
    }
}
