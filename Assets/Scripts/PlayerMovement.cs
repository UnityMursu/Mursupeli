using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] public float slideSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] public float jumpTime = 0.35f;
    [SerializeField] public float jumpTimeCounter;
    [SerializeField] public float normalGravity = 3f;
    [SerializeField] public float pitGravity = 25f;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    private bool isJumping;
    public bool facingRight;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerpendicular;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    

    private bool isOnSlope;

    private enum movementState { idle, walk, jump, fall }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        facingRight = true;
        colliderSize = collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        
        //movement          GetAxis is more smooth
        directionX = Input.GetAxisRaw("Horizontal");
        //rigidBody.velocity = new Vector2(directionX * movementSpeed, rigidBody.velocity.y);

        // Different movement depending on the player's position
        if (IsGrounded() && !isOnSlope)
        {
            rigidBody.velocity = new Vector2(directionX * movementSpeed, 0.0f);
        }
        else if (IsGrounded() && isOnSlope)
        {
            // Same movement speed on slopes as on normal ground
            rigidBody.velocity = new Vector2(movementSpeed * slopeNormalPerpendicular.x * -directionX, movementSpeed * slopeNormalPerpendicular.y * -directionX);
        }
        else if (!IsGrounded())
        {
            rigidBody.velocity = new Vector2(directionX * movementSpeed , rigidBody.velocity.y);
        }

        

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
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
            
        if (Input.GetButton("Vertical") && isOnSlope)
        {
            Debug.Log("slide");
            rigidBody.sharedMaterial = noFriction;
            rigidBody.velocity = new Vector2(slideSpeed * slopeNormalPerpendicular.x * -2, slideSpeed * slopeNormalPerpendicular.y * -2);
            
            //rigidBody.velocity = new Vector2(slideSpeed * slopeNormalPerpendicular.x * -directionX, slideSpeed * slopeNormalPerpendicular.y * -directionX);
        }
    }
    private void FixedUpdate()
    {
        SlopeCheck();
    }

    // Check for whether or not the player is standing on a slope
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, colliderSize.y / 2));

        // Use slope checks to also allow the player to stick on slopes when moving
        HorizontalSlope(checkPos);
        VerticalSlope(checkPos);
        
    }
    // Check for slope horizontally
    private void HorizontalSlope(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, jumpableGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, jumpableGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        } else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }
   
    // Check for slope vertically
    private void VerticalSlope(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, jumpableGround);

        if (hit)
        {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;


            // Debug to show raycast for slope check
            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        // Add friction to prevent the player from sliding down when standing still on slopes
        if (isOnSlope && directionX == 0.0f)
        {
            rigidBody.sharedMaterial = fullFriction;
        }
        else
        {
            rigidBody.sharedMaterial = noFriction;
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
