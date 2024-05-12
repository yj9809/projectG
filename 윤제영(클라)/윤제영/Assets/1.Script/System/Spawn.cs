using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Collider spawnCollider;
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform[] target;
    public Transform[] end;

    private float spwanTime = 1f;
    private float spwanTimer;

    private class TargetSelect
    {
        public int index;
        public bool isUse;
        public float resetTimer;
    }
    private List<TargetSelect> randomTarget = new List<TargetSelect>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < target.Length; i++)
        {
            randomTarget.Add(new TargetSelect { index = i, isUse = false, resetTimer = Time.time});
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

        if (Random.value < 0.9f)
        {
            int random = Random.Range(0, end.Length);
            GameObject npc = Instantiate(this.npc, randomSpawnPosition, Quaternion.identity);
            npc.transform.GetComponent<Npc>().target = end[random];
        }
        else
        {
            int randomIndex = RandomTargetIndex();

            if (randomIndex != -1)
            {
                GameObject newNpc = Instantiate(npc, randomSpawnPosition, Quaternion.identity);
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
            if (targetSelect.index == System.Array.IndexOf(this.target, target))
            {
                targetSelect.isUse = false; 
                break;
            }
        }
    }
}
