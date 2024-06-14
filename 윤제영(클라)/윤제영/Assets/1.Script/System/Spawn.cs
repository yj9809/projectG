using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public PoolManager pool;
    public List<Transform> npcTarget;
    public List<GameObject> elemental;
    public Transform[] elementalTarget;
    public Transform[] end;

    [SerializeField] private float spwanTime = 1f;
    private float spwanTimer;

    public bool onSpawn = false;

    private class TargetSelect
    {
        public int index;
        public bool isUse;
    }
    private class ElementalSelect
    {
        public int index;
        public bool isUse;
    }
    public Queue<Transform> orderTarget = new Queue<Transform>();
    public List<GameObject> customerList = new List<GameObject>();
    private List<TargetSelect> randomTarget = new List<TargetSelect>();
    private List<ElementalSelect> randomElemental = new List<ElementalSelect>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GameManager.Instance.allChair)
        {
            npcTarget.Add(child);
        }
        for (int i = 0; i < npcTarget.Count; i++)
        {
            randomTarget.Add(new TargetSelect { index = i, isUse = false});
        }
        foreach (Transform elemental in GameManager.Instance.elementals)
        {
            this.elemental.Add(elemental.gameObject);
        }
        for (int i = 0; i < elemental.Count; i++)
        {
            randomElemental.Add(new ElementalSelect { index = i, isUse = false });
        }
        customerList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamestate == GameState.Stop)
            return;

        if (onSpawn)
        {
            spwanTimer += Time.deltaTime;
            if (spwanTimer >= spwanTime)
            {
                spwanTimer = 0;

                SpawnNpc();
            }
        }
    }
    private void SpawnNpc()
    {
        if (Random.value < 0.6f)
        {
            int random = Random.Range(0, end.Length);
            GameObject newNpc = pool.CreatNpc();
            newNpc.transform.GetComponent<Npc>().target = end[random];
        }
        else
        {
            int randomIndex = RandomTargetIndex();

            if (randomIndex != -1)
            {
                GameObject newNpc = pool.CreatNpc();
                newNpc.GetComponent<Npc>().target = npcTarget[randomIndex];
                customerList.Add(newNpc);
                randomTarget[randomIndex].isUse = true;
            }
            else
            {
                int random = Random.Range(0, end.Length);
                GameObject newNpc = pool.CreatNpc();
                newNpc.transform.GetComponent<Npc>().target = end[random];
            }
        }
    }
    public void SetElementalTaget(Transform pos)
    {
        for (int i = 0; i < elemental.Count; i++)
        {
            if (!randomElemental[i].isUse)
            {
                elemental[i].transform.GetComponent<ElementalNpc>().target = pos;
                randomElemental[i].isUse = true;
                break;
            }
        }
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
            if (targetSelect.index == this.npcTarget.IndexOf(target))
            {
                targetSelect.isUse = false; 
                break;
            }
        }
    }
    public void SetRandomTarget()
    {
        randomTarget.Add(new TargetSelect { index = npcTarget.Count - 1, isUse = false });
    }
    public void CheckCustomer(GameObject npc)
    {
        if (customerList.Contains(npc))
            customerList.Remove(npc);
    }
}
