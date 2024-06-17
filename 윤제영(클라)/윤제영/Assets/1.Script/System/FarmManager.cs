using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmType
{
    Seed,
    Sprout
}
public class FarmManager : MonoBehaviour
{
    [SerializeField] private GameObject[] farm;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject sprout;
    [SerializeField] private GameObject seeds;

    private FarmType fType = FarmType.Seed;
    // Start is called before the first frame update
    void Start()
    {
        seeds = Instantiate(seed, farm[0].transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Sky.currentTime >= 0.3f && fType == FarmType.Seed)
        {
            Destroy(seeds);
            seeds = Instantiate(sprout, farm[0].transform.GetChild(0));
            fType = FarmType.Sprout;
        }
    }
}
