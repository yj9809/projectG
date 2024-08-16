using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Transform housePos;
    public Transform houseInPos;

    public List<GameObject> partner = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        int allPartner = 1 + GameManager.Instance.Spawn.elemental.Count;
        if (partner.Count == allPartner && GameManager.Instance.gameState != GameState.Stop)
        {
            GameManager.Instance.Ui.Closing();
            GameManager.Instance.gameState = GameState.Stop;
        }
    }
}
