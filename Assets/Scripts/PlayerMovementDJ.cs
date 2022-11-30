using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDJ : MonoBehaviour
{
    public static PlayerMovementDJ instance;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] public float jumpTime = 0.35f;
    [SerializeField] public float jumpTimeCounter;
    private bool isJumping;
    public GameObject BlackScreen;
    public bool facingRight;
    [SerializeField] private bool doubleJump;
    public float blindTime;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip talkSfx;
    [SerializeField] private AudioClip slideSfx;

    private enum movementState { idle, walk, jump, fall }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(blindTime > 0)
        {
            blindTime -= Time.deltaTime;
            BlackScreen.SetActive(true);
        }
        else
        {
            BlackScreen.SetActive(false);
        }

        //movement          GetAxis is more smooth
        directionX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(directionX * movementSpeed, rigidBody.velocity.y);

     if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }

        //jump a
        if (Input.GetButtonDown("Jump") && IsGrounded() && Input.GetAxisRaw("Vertical") == 0)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        animationState();
        playerFlip();

        if (Input.GetButton("Jump") && isJumping == true )
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