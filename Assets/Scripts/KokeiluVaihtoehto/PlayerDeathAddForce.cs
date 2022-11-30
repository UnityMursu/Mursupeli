using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathAddForce : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private GameMaster gameMaster;
    public PlayerMovementAddForce playerScript;
    private float _respawnTime;
    private bool isDead;
    //use after Die()
    public int deathCounter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;


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
                audioSource.PlayOneShot(deathSound, 2F);
                player.GetComponent<PlayerMovementAddForce>().enabled = false;
                isDead = true;

        }
        if (collision.gameObject.CompareTag("Enemy") && playerScript.invincible)
        {
            audioSource.PlayOneShot(deathSound, 2F);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
                audioSource.PlayOneShot(deathSound, 2F);
                player.GetComponent<PlayerMovementAddForce>().enabled = false;
                isDead = true;
        }
    }

    public void Die()
     {
        //animation
        isDead = false;
        player.GetComponent<PlayerMovementAddForce>().enabled = true;
        _respawnTime = 3f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

    private void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
        anim.SetTrigger("life");
    }

}
