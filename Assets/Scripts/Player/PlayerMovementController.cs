﻿using System;
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
    private bool isRolling = false;
    private bool facingRight = true;
    public float rollDuration = 0.16f; // Thời gian lăn (1 giây trong ví dụ này)

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

        Debug.Log("Tag: " + gameObject.tag);

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
        }else if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon = "Bow";
        }else if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon = "Spear";
        }else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon = "Sword";
        }

        if (!isJumping)
        {
            if (moveHorizontal != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) && !isRolling)
                {
                    isRolling = true;
                    gameObject.tag = "Untagged"; // Xóa tag để nhân vật không bị va chạm trong thời gian lăn
                    changeAnimationState(weapon + PlayerSate.Roll);

                    StartCoroutine(RollCoroutine());
                }
                changeAnimationState(weapon + PlayerSate.Run);

                //spriteRenderer.flipX = moveHorizontal < 0;
                if (moveHorizontal > 0 && !facingRight)
                {
                    Flip();
                }else if (moveHorizontal < 0 && facingRight)
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

    // Coroutine để xử lý thời gian lăn và sau đó đứng dậy
    IEnumerator RollCoroutine()
    {
        yield return new WaitForSeconds(rollDuration);

        // Khi thời gian lăn kết thúc, thực hiện hành động dậy
        isRolling = false;
        gameObject.tag = "Player"; // Gán lại tag để nhân vật có thể va chạm
        changeAnimationState(weapon + PlayerSate.Run);

        yield return new WaitForSeconds(0.5f); // Khoảng thời gian đứng dậy sau khi lăn (0.5 giây trong ví dụ này)

        // Thực hiện hành động đứng yên sau khi đứng dậy
        changeAnimationState(weapon + PlayerSate.Idle);
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
