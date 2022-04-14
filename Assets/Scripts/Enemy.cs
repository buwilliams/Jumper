using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float bulletDamage = 10;
    public GameObject healthBar;

    private float startingHealth;
    private Vector3 startingScale;

    void Start()
    {
        startingHealth = health;
        startingScale = healthBar.transform.localScale;
    }

    void Update()
    {
        DestroyOnDeath();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Enemy detected collision with {collision.gameObject.name}");
        if(collision.gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
            Debug.Log($"Enemy taken dmg {health}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Enemy detected trigger collision with {collision.gameObject.tag}");
        if(collision.gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
            UpdateHealthBar();
            Debug.Log($"Enemy taken dmg {health}");
        }
    }

    private void DestroyOnDeath()
    {
        if(health <= 0)
        {
            Debug.Log("Destroying enemy.");
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        float percentRemaining = health / startingHealth;
        float newX = startingScale.x * percentRemaining;
        Debug.Log($"Updating healthbar starting-{startingScale.x} percentRemaining-{percentRemaining} newX-{newX}");
        healthBar.transform.localScale = new Vector3(newX, startingScale.y, startingScale.z);
    }
}
