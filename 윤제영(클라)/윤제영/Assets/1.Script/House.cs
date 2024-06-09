using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Transform housePos;
    public Transform houseInPos;

    public List<GameObject> partner = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int allPartner = 1 + GameManager.Instance.Spawn.elemental.Count;
        if (partner.Count == allPartner -1)
        {
            GameManager.Instance.gamestate = GameState.Stop;
        }
    }
}
