using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 20f;

    void Start()
    {
        if (transform.rotation.y > 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") return;

        Debug.Log($"Collision with {collision.gameObject.name} detected, destroying myself!");
        Destroy(gameObject);
    }
}
