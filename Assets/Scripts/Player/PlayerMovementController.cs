﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce;
    [SerializeField] private int leftLimitationOffset;
    private bool isJumping = false;
    String currentState = "";
    String weapon = "";
    Vector3 LeftLimitation;
    enum PlayerSate
    {
        Run, Roll, Jump, Idle, Die, Throw, Attack, Attack1, Attack2, Attack3
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

        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            changeAnimationState(weapon + PlayerSate.Attack1);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            changeAnimationState(weapon + PlayerSate.Idle);
        }

        //Debug.Log(weapon + PlayerSate.Run.ToString());
        if (!isJumping)
        {
            if (moveHorizontal != 0)
            {
                changeAnimationState(weapon + PlayerSate.Run.ToString());
                spriteRenderer.flipX = moveHorizontal < 0;
                Debug.Log(weapon + PlayerSate.Run.ToString());
            }
            else
            {
                changeAnimationState(weapon + PlayerSate.Idle.ToString());
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
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

            isJumping = false;
        }
    }

    void changeAnimationState(String newPlayerState)
    {
        if (currentState.Equals(newPlayerState))
        {
            return;
        }

        animator.Play(newPlayerState);

        currentState = newPlayerState;
    }
}