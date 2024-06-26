using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmType
{
    Seed,
    Sprout,
    Crops,
    Null
}
public class FarmManager : MonoBehaviour
{
    [SerializeField] private GameObject[] farm;
    [SerializeField] private GameObject[] seeds;
    [SerializeField] private GameObject[] crops;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject sprout;

    private GameManager gm;
    private FarmType fType = FarmType.Null;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Sky.currentTime >= 0.3f && fType == FarmType.Seed)
            SeedGrowth();
        else if (GameManager.Instance.Sky.currentTime >= 0.6f && fType == FarmType.Sprout)
            SproutGrowth();
    }
    public void PlantingSeed()
    {
        Debug.Log("½ÇÇà");
        for (int i = 0; i < farm.Length; i++)
        {
            seeds[i]= Instantiate(seed, farm[i].transform.GetChild(0));
            seeds[i].name = seed.name;
            fType = FarmType.Seed;
        }
    }
    private void SeedGrowth()
    {
        for (int i = 0; i < farm.Length; i++)
        {
            Destroy(seeds[i]);
            seeds[i] = Instantiate(sprout, farm[i].transform.GetChild(0));
            seeds[i].name = sprout.name;
            fType = FarmType.Sprout;
        }
    }
    private void SproutGrowth()
    {
        for (int i = 0; i < farm.Length; i++)
        {
            float randomValue = Random.value;
            int random = randomValue < 0.7f ? 0 : randomValue < 0.85f ? 1 : randomValue < 0.95f ? 2 : 3;
            Destroy(seeds[i]);
            seeds[i] = Instantiate(crops[random], farm[i].transform.GetChild(0));
            seeds[i].name = crops[random].name;
            fType = FarmType.Crops;
        }
    }
    public void Harvest()
    {
        for (int i = 0; i < farm.Length; i++)
        {
            switch (seeds[i].name)
            {
                case "Wheat":
                    gm.WheatEa += 1;
                    break;
                case "Potato":
                    gm.PotatoEa += 1;
                    break;
                case "Tomato":
                    gm.TomatoEa += 1;
                    break;
                case "ButterMushroom":
                    gm.ButterMushroomEa += 1;
                    break;
            }
            Destroy(seeds[i]);
        }
    }
}
