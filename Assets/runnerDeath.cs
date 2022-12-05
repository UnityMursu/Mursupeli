using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class runnerDeath : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private GameMaster gameMaster;
    public RunnerMovement playerScript;
    private float _respawnTime;
    private bool isDead;
    public int deathCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        _respawnTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            anim.SetTrigger("death");
            _respawnTime -= Time.deltaTime;
            if (_respawnTime < 0f)
            {
                Die();
                deathCounter++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject trap = GameObject.FindWithTag("Trap");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //player.GetComponent<PlayerMovement>().enabled = false;
            isDead = true;

        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            //player.GetComponent<PlayerMovement>().enabled = false;
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            isDead = true;
        }
    }

    public void Die()
    {
        isDead = false;
        player.GetComponent<RunnerMovement>().enabled = true;
        _respawnTime = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
        anim.SetTrigger("life");
    }
}
