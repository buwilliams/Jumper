using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        Debug.Log("Creating enemy.");
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
