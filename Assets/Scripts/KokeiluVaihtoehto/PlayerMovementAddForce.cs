using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAddForce : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private BoxCollider2D _collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public static PlayerMovementAddForce instance;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX;
    private float normalDrag;
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
    [SerializeField] private PhysicsMaterial2D normalFriction;
    private bool isJumping;
    private bool isSliding;
    public bool facingRight;
    public bool onPlat;
    public bool invincible;
    public bool onIce;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerpendicular;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    

    public bool isOnSlope;

    private enum movementState { idle, walk, jump, fall }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip talkSfx;
    [SerializeField] private AudioClip slideSfx;
    private float jumpSfxTimer;
    private float slideSfxTimer;
    private float talkSfxTimer;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        facingRight = true;
        isSliding = false;
        onPlat = false;
        colliderSize = _collider.size;
        normalDrag = rigidBody.drag;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //movement          GetAxis is more smooth
        directionX = Input.GetAxisRaw("Horizontal");
        //rigidBody.velocity = new Vector2(directionX * movementSpeed, rigidBody.velocity.y);

        // Different movement depending on the player's position
        /*
        if (IsGrounded() && !isOnSlope && !onPlat)
        {
            rigidBody.velocity = new Vector2(directionX * movementSpeed, 0.0f);
        }
        else if (IsGrounded() && isOnSlope && !onPlat)
        {
            // Same movement speed on slopes as on normal ground
            rigidBody.velocity = new Vector2(movementSpeed * slopeNormalPerpendicular.x * -directionX, movementSpeed * slopeNormalPerpendicular.y * -directionX);
        }
        else if (!IsGrounded() && !onPlat)
        {
            rigidBody.velocity = new Vector2(directionX * movementSpeed , rigidBody.velocity.y);
        }
        else if (onPlat)
        {
            rigidBody.velocity = new Vector2(directionX * movementSpeed, rigidBody.velocity.y);
        }
        */

        

        if (Input.GetButtonDown("Jump") && !isSliding && Input.GetAxisRaw("Vertical") == 0)
        {
            if (IsGrounded())
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                
                audioSource.PlayOneShot(jumpSfx, 0.7F);
            }
        }
        
        jumpSfxTimer -= Time.deltaTime;
        //jump a
        if (Input.GetButtonDown("Jump") && IsGrounded() && Input.GetAxisRaw("Vertical") == 0 && !isSliding)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpSfx, 0.7F);
        } 
        else if (isSliding && Input.GetButton("Jump"))
        {
            Debug.Log("slidejump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            if (jumpSfxTimer < 0) {
                audioSource.PlayOneShot(jumpSfx, 0.7F);
                jumpSfxTimer = 0.3f;
            }
            //rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpForce);
            //rigidBody.AddForce(new Vector2(slideSpeed * 10f, slideSpeed * 10f));
            rigidBody.velocity = transform.right * slideSpeed;  
        }

        animationState();
        playerFlip();
        

        if (Input.GetButton("Jump") && isJumping == true && !isSliding)
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
        talkSfxTimer -= Time.deltaTime;
        if (Input.GetButton("Fire3") && talkSfxTimer < 0)
        {
            audioSource.PlayOneShot(talkSfx, 0.3F);
            talkSfxTimer = 1f;
        }
    }

     private void Awake()
    {
        instance = this;
    }
    
    private void FixedUpdate()
    {

        if (rigidBody.velocity.x < movementSpeed && directionX > 0) 
            {
                if (IsGrounded() && !isOnSlope && !onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40, movementSpeed));
                }
                else if (IsGrounded() && isOnSlope && !onPlat)
                {
                    // Same movement speed on slopes as on normal ground
                    rigidBody.AddForce(new Vector2(50 * slopeNormalPerpendicular.x * -directionX, 50 * slopeNormalPerpendicular.y * -directionX));
                }
                else if (!IsGrounded() && !onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40 , rigidBody.velocity.y));
                }
                else if (onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40, rigidBody.velocity.y));
                }
            }
        else if (rigidBody.velocity.x > -movementSpeed && directionX < 0)
        {
            if (IsGrounded() && !isOnSlope && !onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40, 0.0f));
                }
                else if (IsGrounded() && isOnSlope && !onPlat)
                {
                    // Same movement speed on slopes as on normal ground
                    rigidBody.AddForce(new Vector2(50 * slopeNormalPerpendicular.x * -directionX, 50 * slopeNormalPerpendicular.y * -directionX));
                }
                else if (!IsGrounded() && !onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40 , rigidBody.velocity.y));
                }
                else if (onPlat)
                {
                    rigidBody.AddForce(new Vector2(directionX * 40, rigidBody.velocity.y));
                }
        }
        
        slideSfxTimer -= Time.deltaTime;
        if (Input.GetButton("Fire2") && isOnSlope)
        {
            // Make the player slide down slopes when down is pressed
            rigidBody.sharedMaterial = noFriction;
            isSliding = true;
            invincible = true;

            // Kesken. En keksi mill� arvoilla tarkistan, onko laskeutuuko m�ki vasemmalle vai oikealle (Koodin tarkoitus on liu'uttaa mursu aina m�ke� alasp�in)
            if (slopeNormalPerpendicular.y > 0)
            {
                rigidBody.AddForce(new Vector2(slideSpeed * slopeNormalPerpendicular.x * -2, slideSpeed * slopeNormalPerpendicular.y * -2));
            } else if (slopeNormalPerpendicular.y < 0)
            {
                rigidBody.AddForce(new Vector2(slideSpeed * slopeNormalPerpendicular.x * 2, slideSpeed * slopeNormalPerpendicular.y * 2));
            }
            if (isSliding && slideSfxTimer < 0)
            {
                audioSource.PlayOneShot(slideSfx, 0.3F);
                slideSfxTimer = 0.41f;
            } 


            if (!facingRight && slopeNormalPerpendicular.y > 0)
            {
                Flip();
            } else if (facingRight && slopeNormalPerpendicular.y < 0)
            {
                Flip();
            }
            //rigidBody.velocity = new Vector2(slideSpeed * slopeNormalPerpendicular.x * -directionX, slideSpeed * slopeNormalPerpendicular.y * -directionX);
        }
        else if (isOnSlope && directionX == 0.0f)
        {
            // Add friction to prevent the player from sliding down when standing still on slopes
            rigidBody.sharedMaterial = fullFriction;
            isSliding = false;
            invincible = false;

            //slideSfx.Stop();

        }
        else if (isOnSlope && (directionX < 0.0f || directionX > 0.0f) || onIce)
        {
            rigidBody.sharedMaterial = noFriction;
            isSliding = false;
            invincible = false;
            //slideSfx.Stop();
            if (!isSliding || isJumping)
            {
                //slideSfx.Stop();
            }
        }
        else
        {
            rigidBody.sharedMaterial = normalFriction;
            isSliding = false;
            invincible = false;
            //slideSfx.Stop();
            if (!isSliding || isJumping)
            {
                //slideSfx.Stop();
            }
        }


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

    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.CompareTag("Platform"))
        {
            onPlat = true;
        }
        if(info.gameObject.name == "Ice")
        {
            rigidBody.drag = 0.01f;
            onIce = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D info)
    {
        onPlat = false;
        if (info.gameObject.name == "Ice")
        {
            rigidBody.drag = normalDrag;
            onIce = false;
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

        

    }

    

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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
