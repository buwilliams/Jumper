using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        gameObject.transform.position = player.position + new Vector3(0, 0, -5);
    }
}
