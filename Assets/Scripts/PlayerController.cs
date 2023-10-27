using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetInteger("State", 2); // State 2: Jump
        }
        // Set animator state based on movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("State", 1); // State 1: Move
            // Flip the sprite when moving left
            spriteRenderer.flipX = moveHorizontal < 0;
        }
        else
        {
            animator.SetInteger("State", 0); // State 0: Idle
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state when landing
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
