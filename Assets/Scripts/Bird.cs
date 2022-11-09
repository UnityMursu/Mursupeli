using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    private float attackTimer;
    private float waitTime;
    private Vector3 startPosition;
    private bool playerInRange;
    private bool turnAround;
    private bool canMove;
    ScorptionAttack attackPoint;
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    private bool isGrounded;


    void Start()
    {
        attackPoint = gameObject.GetComponentInChildren<ScorptionAttack>();
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        attackTimer = 1f;
        waitTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Will move for given distance at given speed until told not to move
        /*if(canMove == true){
            if (turnAround == false){
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
                if(startPosition.x + moveDistance < transform.position.x){
                    turnAround = true;
                    transform.Rotate(0f, 180f, 0f);
                }
            }
            else if(turnAround == true) 
            {
                transform.position = transform.position + new Vector3(-1 *speed * Time.deltaTime,0,0);
                if(startPosition.x > transform.position.x){
                    turnAround = false;
                    transform.Rotate(0f, 180f, 0f);
                }
            }
        }*/
        

        if(canMove == true){
            if (turnAround == false){
                transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
                if(startPosition.y + moveDistance < transform.position.y){
                    turnAround = true;
                    //transform.Rotate(0f, 180f, 0f);
                }
            }
            else if(turnAround == true) 
            {
                transform.position = transform.position + new Vector3(0 ,-1 *speed * Time.deltaTime,0);
                if(startPosition.y > transform.position.y){
                    turnAround = false;
                    transform.Rotate(0f, 180f, 0f);
                }
            }

            if (startPosition.x < transform.position.x)
                { 
                transform.position = transform.position + new Vector3(-speed * Time.deltaTime, 0 , 0);
                }
            if (startPosition.x > transform.position.x)
            { 
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0 , 0);
            }
        }


        //Will call attack function every second and use attack animation until wait time is over
        if(waitTime > 0) {
            if (player.position.x > startPosition.x - attackDistance && player.position.x < startPosition.x + attackDistance)
            {
                DashAttack();
            }
        }
        
        //wait time will count down if player is not in range
        if(playerInRange == false){
            waitTime -= Time.deltaTime;
            if(waitTime <= 0)
            {
                canMove = true;
            }
        }

    }


    void DashAttack()
    {
        float distanceFromPlayerX = player.position.x - transform.position.x;
        float distanceFromPlayerY = player.position.y - transform.position.y;
        transform.position = transform.position + new Vector3(distanceFromPlayerX*2*Time.deltaTime,0,0);
        transform.position = transform.position + new Vector3(0,distanceFromPlayerY*2*Time.deltaTime,0);
    }

    //Checks if something comes in collision range. Tells not to move if it is player inside triggers collision range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            canMove = false;

            waitTime = 2.5f;
        }
    }

    //If an object exits triggers collision range will check if it was player and act appropriately 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            waitTime = 2.5f;
            playerInRange = false;
        }
    }
}
