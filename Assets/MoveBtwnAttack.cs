using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBtwnAttack : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;

    [SerializeField] private float speed;
    [SerializeField] private Vector3[] positions;
    private bool playerInRange;
    private Vector3 startPosition;
    private float directionX;
    private int index;
    [SerializeField] private float moveDistance;
    private bool turnAround;
    private float waitTime;
    private bool canMove;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInRange = false;
        startPosition = transform.position;
        turnAround = false;
        waitTime = 2.5f;
        Debug.Log(startPosition);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
        
        //rigidBody.velocity = new Vector2(directionX * speed, rigidBody.velocity.y);
        // startPosition.x + moveDistance >= transform.position.x && 
        if(canMove == true){
            Debug.Log("player not in range");
            if (turnAround == false){
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
                Debug.Log("moving");
                if(startPosition.x + moveDistance < transform.position.x){
                    turnAround = true;
                }
            }
            else if(turnAround == true) 
            {
                transform.position = transform.position + new Vector3(-1 *speed * Time.deltaTime,0,0);
                if(startPosition.x > transform.position.x){
                    turnAround = false;
                }
            }
            /*if (transform.position == positions[index]) {
                if (index == positions.Length - 1){
                    index = 0;
                    
                    transform.Rotate(0f, 180f, 0f);
                }
                else {
                    index++;
                    transform.Rotate(0f, 180f, 0f);
                }
            }*/
        }
        
        
            waitTime -= Time.deltaTime;
            if(playerInRange == false){
                if(waitTime <= 0)
                {
                    canMove = true;
                }
            }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            canMove = false;
            Debug.Log("Player in range");
            waitTime = 2.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("player left");
        if (collision.gameObject.CompareTag("Player"))
        {
            waitTime = 2.5f;
            playerInRange = false;
        }
    }
}
