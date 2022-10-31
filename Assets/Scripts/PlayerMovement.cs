using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] public float jumpTime = 0.35f;
    [SerializeField] public float jumpTimeCounter;
    private bool isJumping;
    public bool facingRight;

    private enum movementState { idle, walk, jump, fall }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //movement          GetAxis is more smooth
        directionX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(directionX * movementSpeed, rigidBody.velocity.y);

        //jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        animationState();
        playerFlip();

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                isJumping = false;
            }
   
        }
        
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
            
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void animationState()
    {
        movementState state;

        if (directionX > 0f)
        {
            state = movementState.walk;
        }
        else if (directionX < 0f)
        {
            state = movementState.walk;
        }
        else
        {
            state = movementState.idle;
        }

        if (rigidBody.velocity.y > .1f)
        {
            state = movementState.jump;
        }
        else if (rigidBody.velocity.y < -.1f)
        {
            state = movementState.fall;
        }

        animator.SetInteger("state", (int)state);
    }

    private void playerFlip()
    {
        if (directionX > 0f && !facingRight)
        {
            Flip();
        }
        else if (directionX < 0f && facingRight)
        {
            Flip();
        }
       

    }

    private void Flip()
    {
        // Flip the way the player and ice point are facing
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
       
    }

}
