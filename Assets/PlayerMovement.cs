using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask jumpableGround;


    private float directionX;
    //public also works, but not good practice
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float jumpForce = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
