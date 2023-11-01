using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce;
    [SerializeField] private int leftLimitationOffset;
    public bool isJumping = false;
    String currentState = "";
    public String weapon = "";
    private bool facingRight = true;

    Vector3 LeftLimitation;
    public enum PlayerSate
    {
        Run, Roll, Jump, Idle, Die
    };

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

        if (transform.position.x < LeftLimitation.x)
        {
            transform.position = new Vector3(LeftLimitation.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon = "";
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon = "Bow";
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon = "Spear";
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon = "Sword";
        }

        if (!isJumping)
        {
            if (moveHorizontal != 0)
            {

                changeAnimationState(weapon + PlayerSate.Run);

                //spriteRenderer.flipX = moveHorizontal < 0;
                if (moveHorizontal > 0 && !facingRight)
                {
                    Flip();
                }
                else if (moveHorizontal < 0 && facingRight)
                {
                    Flip();
                }
                //Debug.Log(weapon + PlayerSate.Run.ToString());
            }
            else
            {
                changeAnimationState(weapon + PlayerSate.Idle);
            }
        }

        Debug.Log("isJumping: " + isJumping);

        // Jumping
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                changeAnimationState(weapon + PlayerSate.Jump.ToString());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state when landing
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounds colliderBounds = collision.gameObject.GetComponent<BoxCollider2D>().bounds;
            // Lấy vị trí mép bên trái của Collider
            LeftLimitation = new Vector3(colliderBounds.min.x + leftLimitationOffset, colliderBounds.center.y, colliderBounds.center.z);
            Debug.Log("OnGround");
            isJumping = false;
        }
    }

    public void changeAnimationState(String newPlayerState)
    {
        if (currentState.Equals(newPlayerState))
        {
            return;
        }

        animator.Play(newPlayerState);

        currentState = newPlayerState;
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
