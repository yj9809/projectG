using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private GameObject[] farm;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject sprout;

    public GameObject seeds;
    // Start is called before the first frame update
    void Start()
    {
        seeds = Instantiate(seed, farm[0].transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Sky.currentTime >= 0.3f)
        {
            seed = Instantiate(sprout, farm[0].transform.GetChild(0));
        }
    }
}
