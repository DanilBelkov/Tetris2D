using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBricks : MonoBehaviour
{
    public GameObject[] Bricks;

    public void CreateBrick()
    {
        Instantiate(Bricks[Random.Range(0, Bricks.Length)], transform.position, Quaternion.identity);
    }

}
