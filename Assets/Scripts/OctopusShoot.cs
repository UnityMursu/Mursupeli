using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusShoot : MonoBehaviour
{
    [Header("For Patrol")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("other")]
    private Rigidbody2D enemyRB;

    [Header("For Shooting")]
    public float timeBTWShots;
    public float shootSpeed;
    public GameObject bullet;
    [SerializeField] Transform player, shootPos;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    public GameObject BlackScreen;
    private bool isGrounded;
    private bool canShoot;

    [Header("Detect Player")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;
   


   void Start()
   {
        enemyRB = GetComponent<Rigidbody2D>();
        canShoot= true;
   }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position,boxSize,0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position,lineOfSight, 0 , playerLayer);
        
        if (!canSeePlayer && isGrounded)
        {
            Patrolling();
        }
        if (canSeePlayer && isGrounded && canShoot)
        {
            StartCoroutine(ShootAttack());
        }
        
        FlipTowardsPlayer();
    }

   void Patrolling()
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

     IEnumerator ShootAttack()
    {
        canShoot = false;
       yield return new WaitForSeconds(timeBTWShots);
       GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

       newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * moveSpeed * Time.fixedDeltaTime, 0f);
       canShoot = true;
    }

    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition<0 && facingRight == true)
        {
            Flip();
        }
        else if (playerPosition>0 && !facingRight)
        {
            Flip();
        }
    }
    
    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }

       public IEnumerator BlindTimer()
     {
        Debug.Log("blinded");
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("aika");
        BlackScreen.SetActive(false);
        Debug.Log("aika2");
     }
 
}
