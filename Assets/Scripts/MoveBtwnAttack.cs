using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBtwnAttack : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;

    [SerializeField] private float speed;
    private bool playerInRange;
    private Vector3 startPosition;
    [SerializeField] private float moveDistance;
    private bool turnAround;
    private float waitTime;
    private bool canMove;
    ScorptionAttack attackPoint;
    private float attackTimer;
    Animator scorpAnimator;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        waitTime = 0f;
        attackTimer = 1f;
        scorpAnimator = gameObject.GetComponent<Animator>();
        attackPoint = gameObject.GetComponentInChildren<ScorptionAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // Will move for given distance at given speed until told not to move
        if(canMove == true){
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
        }
        
        //Will call attack function every second and use attack animation until wait time is over
        if(waitTime > 0) {
            attackTimer -= Time.deltaTime;
            scorpAnimator.ResetTrigger("Attack");
            if(attackTimer < 0){
                attackPoint.Attack();
                attackTimer = 1f;
                scorpAnimator.SetTrigger("Attack");
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
