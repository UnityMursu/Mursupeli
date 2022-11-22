using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private GameObject player;
    private GameMaster gameMaster;
    public PlayerMovement playerScript;
    private float _respawnTime;
    private bool isDead;

    private void Start()
    {   
        isDead = false;
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        _respawnTime = 3f;
    }

    private void Update() 
    {
        if (isDead) {
            _respawnTime -= Time.deltaTime;
            if (_respawnTime < 0f) {
                    Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject trap = GameObject.FindWithTag("Trap");
        if (collision.gameObject.CompareTag("Enemy") && !playerScript.invincible)
        {
                player.GetComponent<PlayerMovement>().enabled = false;
                isDead = true;

        }
        if (collision.gameObject.CompareTag("Enemy") && playerScript.invincible)
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
                player.GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
        }
    }

    public void Die()
     {
        //animation
        isDead = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        _respawnTime = 3f;
        LoadLastCheckpoint();
     }

    private void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
    }

}
