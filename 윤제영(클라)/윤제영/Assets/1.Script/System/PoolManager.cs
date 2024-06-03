using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Npc;
    [SerializeField] private Collider spawnCollider;

    [SerializeField] private Queue<GameObject> poolNpc = new Queue<GameObject>();
    
    public GameObject CreatNpc()
    {
        Vector3 randomSpawnPosition = GetRandomPositionInBounds(spawnCollider.bounds);
        if (poolNpc.Count > 0)
        {
            GameObject newNpc = poolNpc.Dequeue();
            newNpc.transform.position = spawnCollider.transform.position;
            newNpc.SetActive(true);
            return newNpc;
        }
        else
        {
            int random = Random.Range(0, Npc.Length);
            GameObject newNpc = Instantiate(Npc[random], randomSpawnPosition, Quaternion.identity);
            newNpc.transform.SetParent(GameObject.Find("Npcs").transform);
            return newNpc;
        }
    }
    public void ReturnNpc(GameObject npc)
    {
        npc.SetActive(false);
        poolNpc.Enqueue(npc);
    }
    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
