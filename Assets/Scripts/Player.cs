using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 60f;
    public float moveSpeed = 1f;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if(horizontalMovement > 0 || horizontalMovement < 0)
        {
            rb.AddForce(new Vector3(horizontalMovement * moveSpeed, 0), ForceMode2D.Impulse);
        }

        if(!isJumping && (verticalMovement > 0 || verticalMovement < 0))
        {
            rb.AddForce(new Vector3(0, verticalMovement * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Enter platform");
            isJumping = false;
        }

        if (collision.gameObject.tag == "Exit")
        {
            Debug.Log("Exit detected");
            Application.Quit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Leaving platform");
            isJumping = true;
        }
    }
}
