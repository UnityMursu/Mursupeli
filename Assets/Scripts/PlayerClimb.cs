using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private bool isTouchingWall;
    private float speed = 8f;
    private float vertical;
    private bool isClimbing;

    private float originalGravityScale;

    [SerializeField] private Rigidbody2D rigidBody;
    
    private void Start()
    {
        //saves original player gravityScale for later
        originalGravityScale = rigidBody.gravityScale;
    }

    private void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        //vertical = up or down buttons are held
        if (isTouchingWall && vertical != 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, vertical * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if player is touching a climbable wall
        if (collision.CompareTag("Climbable"))
        {
            isTouchingWall = true;
            //sets player gravity scale to zero and y velocity to zero
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            isTouchingWall = false;
            isClimbing = false;
            //sets player gravity scale back to normal
            rigidBody.gravityScale = originalGravityScale;
        }
    }
}