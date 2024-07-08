using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacter : MonoBehaviour
{
    private GameManager gm;
    private FarmManager fm;
    private NavMeshAgent nm;
    private Animator ani;

    public Transform target;

    private bool isMove = false;
    private bool goHome = false;
    private bool goCounter = false;
    private bool goFarm = false;
    private bool plant = true;
    private void Awake()
    {
        nm = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        gm = GameManager.Instance;
        fm = FindObjectOfType<FarmManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gamestate == GameState.Stop)
            return;

        if (isMove)
            nm.SetDestination(target.position);
    }
    private int KitchenCheck()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        int rotaY = 0;
        if (Physics.Raycast(pos, Vector3.forward, 2f, LayerMask.GetMask("Kitchen")))
            rotaY = 0;
        else if (Physics.Raycast(pos, Vector3.back, 2f, LayerMask.GetMask("Kitchen")))
            rotaY = 180;
        Debug.Log(rotaY);
        return rotaY;
    }
    public void GoHouse()
    {
        ani.SetBool("Move", true);
        target = gm.House.housePos;
        goHome = true;
        isMove = true;
    }
    public void GoFarm()
    {
        transform.position = gm.House.housePos.position;
        nm.enabled = true;
        ani.SetBool("Move", true);
        target = fm.transform;
        goHome = false;
        goFarm = true;
        isMove = true;
    }
    public void GoCounter()
    {
        target = gm.ProtagonistPos;
        ani.SetBool("Move", true);
        goCounter = true;
        goFarm = false;
        isMove = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House" && goHome)
        {
            ani.SetBool("Move", false);
            nm.enabled = false;
            isMove = false;
            plant = true;
            transform.position = gm.House.houseInPos.position;
            gm.House.partner.Add(gameObject);
        }
        if (other.gameObject.tag == "Counte" && goCounter)
        {
            ani.SetBool("Move", false);
            isMove = false;
            goCounter = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.position = gm.ProtagonistPos.position;
        }
        if (other.gameObject.tag == "Farm" && goFarm)
        {
            if (plant)
            {
                plant = false;
                ani.SetBool("Move", false);
                ani.SetTrigger("Watering");
                isMove = false;
                goFarm = false;
                fm.PlantingSeed();
                Invoke("GoCounter", 4f);
            }
            else if(!plant)
            {
                ani.SetBool("Move", false);
                ani.SetTrigger("Harvest");
                isMove = false;
                goFarm = false;
                fm.Harvest();
                Invoke("GoHouse", 4f);
            }
        }
    }
}
