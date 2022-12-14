using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathWater : MonoBehaviour
{
    private GameObject player;
    private GameMaster gameMaster;
    public PlayerMovement playerScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject trap = GameObject.FindWithTag("Trap");

        if (collision.gameObject.CompareTag("Enemy") && !playerScript.invincible)
        {
                Die();
        }
        if (collision.gameObject.CompareTag("Enemy") && playerScript.invincible)
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
     {
        //animation
        LoadLastCheckpoint();
     }

    private void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
    }

}
