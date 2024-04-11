using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTile(Transform tileTransform)
    {
        Instantiate(tile, tileTransform.position, Quaternion.identity);
    }
    public void SetWall(Transform tileTransform)
    {
        Vector3 pos = new Vector3(tileTransform.position.x, tileTransform.position.y, transform.position.z - 7);
        Instantiate(wall, pos, Quaternion.identity);
    }
}
