using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shooting!");
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log($"Rotation {firePoint.rotation.y}");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
