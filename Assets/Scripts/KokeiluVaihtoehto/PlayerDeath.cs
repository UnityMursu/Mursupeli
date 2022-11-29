using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private GameMaster gameMaster;
    public PlayerMovement playerScript;
    private float _respawnTime;
    private bool isDead;
    //use after Die()
    public int deathCounter;
    [SerializeField] private AudioSource deathSound;
    
    private void Start()
    {   
        isDead = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        _respawnTime = 3f;
    }

    private void Update() 
    {
        if (isDead) {
            anim.SetTrigger("death");
            _respawnTime -= Time.deltaTime;
            if (_respawnTime < 0f) {
                    Die();
                    deathCounter++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject trap = GameObject.FindWithTag("Trap");
        if (collision.gameObject.CompareTag("Enemy") && !playerScript.invincible)
        {
                //player.GetComponent<PlayerMovement>().enabled = false;
                isDead = true;

        }
        if (collision.gameObject.CompareTag("Enemy") && playerScript.invincible)
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
                //player.GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
        }
    }

    public void Die()
     {
        //animation
        isDead = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        deathSound.Play();
        _respawnTime = 3f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

    private void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
        anim.SetTrigger("life");
    }

}
