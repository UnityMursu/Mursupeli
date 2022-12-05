using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] public float jumpTime = 0.35f;
    [SerializeField] public float jumpTimeCounter;
    [SerializeField] public float normalGravity = 3f;
    private bool isJumping;
    public bool facingRight;
    public bool goinRight;
    private enum movementState { idle, walk, jump, fall }
    [SerializeField] private AudioSource jumpSfx;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        facingRight = true;
        goinRight = true;
        rigidBody.gravityScale = normalGravity;

    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        if (goinRight)
        {
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jumpSfx.Play();
            }
        }

        animationState();

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce / 2);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            goinRight = !goinRight;
            facingRight = !facingRight;
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void animationState()
    {
        movementState state;

        if (rigidBody.velocity.x > 0f)
        {
            state = movementState.walk;
            spriteRenderer.flipX = false;
     
        }
        else if (rigidBody.velocity.x < 0f)
        {
            state = movementState.walk;
            spriteRenderer.flipX = true;
            
        }
        else
        {
            state = movementState.idle;
        }

        if (rigidBody.velocity.y > .1f && !IsGrounded())
        {
            state = movementState.jump;
        }
        else if (rigidBody.velocity.y < -.1f && !IsGrounded())
        {
            state = movementState.fall;
        }

        animator.SetInteger("state", (int)state);
    }
}
