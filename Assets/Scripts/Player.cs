using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float jumpForce = 60f;
    public float moveSpeed = 1f;
    private float maxHorizontalVelocity = 10f;
    private float maxVerticalVelocity = 100f;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    bool isJumping = false;
    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        MaybeFlipSprite();
    }

    void FixedUpdate()
    {
        if(horizontalMovement > 0 || horizontalMovement < 0)
        {
            rb.AddForce(new Vector3(horizontalMovement * moveSpeed, 0), ForceMode2D.Impulse);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if(!isJumping && (verticalMovement > 0 || verticalMovement < 0))
        {
            rb.AddForce(new Vector3(0, verticalMovement * jumpForce), ForceMode2D.Impulse);
        }

        ClampVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collider 1 {collision.collider.GetType()}, Collider 2 {collision.otherCollider.GetType()}");
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Enter platform");
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
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
            animator.SetBool("isJumping", isJumping);
        }
    }

    private void MaybeFlipSprite()
    {
        if (facingRight && horizontalMovement < 0) // facing right, turn left
        {
            Debug.Log("Turning to face left");
            //GetComponent<SpriteRenderer>().flipX = true;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (!facingRight && horizontalMovement > 0) // facing left, turn right
        {
            Debug.Log("Turning to face right");
            //GetComponent<SpriteRenderer>().flipX = false;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }

    private void ClampVelocity()
    {
        if(Mathf.Abs(rb.velocity.x) > maxHorizontalVelocity || Mathf.Abs(rb.velocity.y) > maxVerticalVelocity)
        {
            float x = rb.velocity.x < 0 ? Mathf.Max(rb.velocity.x, maxHorizontalVelocity * -1) : Mathf.Min(rb.velocity.x, maxHorizontalVelocity);
            float y = rb.velocity.y < 0 ? Mathf.Max(rb.velocity.y, maxVerticalVelocity * -1) : Mathf.Min(rb.velocity.y, maxVerticalVelocity);
            Debug.Log($"Clamping velocity, x({x}) y({y})");
            rb.velocity = new Vector2(x, y);
        }
    }
}
