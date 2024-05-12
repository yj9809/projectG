using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Collider spawnCollider;
    [SerializeField] private GameObject[] npc;
    public List<Transform> target;
    public Transform[] end;

    private float spwanTime = 1f;
    private float spwanTimer;

    private class TargetSelect
    {
        public int index;
        public bool isUse;
    }
    private List<TargetSelect> randomTarget = new List<TargetSelect>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < target.Count; i++)
        {
            randomTarget.Add(new TargetSelect { index = i, isUse = false});
        }
    }

    // Update is called once per frame
    void Update()
    {
        spwanTimer += Time.deltaTime;
        if (spwanTimer >= spwanTime)
        {
            spwanTimer = 0;

            SpawnNpc();
        }
    }
    private void SpawnNpc()
    {
        Vector3 randomSpawnPosition = GetRandomPositionInBounds(spawnCollider.bounds);

        if (Random.value < 0.4f)
        {
            int random = Random.Range(0, end.Length);
            int randomNpc = Random.Range(0, npc.Length);
            GameObject newNpc = Instantiate(npc[randomNpc], randomSpawnPosition, Quaternion.identity);
            newNpc.transform.GetComponent<Npc>().target = end[random];
        }
        else
        {
            int randomIndex = RandomTargetIndex();

            if (randomIndex != -1)
            {
                int randomNpc = Random.Range(0, npc.Length);
                GameObject newNpc = Instantiate(npc[randomNpc], randomSpawnPosition, Quaternion.identity);
                newNpc.GetComponent<Npc>().target = target[randomIndex];
                randomTarget[randomIndex].isUse = true;
            }
        }
    }
    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
    private int RandomTargetIndex()
    {
        List<int> availableTargets = new List<int>();
        for (int i = 0; i < randomTarget.Count; i++)
        {
            if (!randomTarget[i].isUse)
                availableTargets.Add(i);
        }

        if (availableTargets.Count == 0)
            return -1;

        return availableTargets[Random.Range(0, availableTargets.Count)];
    }
    public void ResetTarget(Transform target)
    {
        foreach (TargetSelect targetSelect in randomTarget)
        {
            if (targetSelect.index == this.target.IndexOf(target))
            {
                targetSelect.isUse = false; 
                break;
            }
        }
    }
    public void SetRandomTarget()
    {
        randomTarget.Add(new TargetSelect { index = target.Count - 1, isUse = false });
    }
}
