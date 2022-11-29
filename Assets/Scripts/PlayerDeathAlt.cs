using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathAlt : MonoBehaviour
{
    private GameObject player;
    private GameMaster gameMaster;
     [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject trap = GameObject.FindWithTag("Trap");

        if (collision.gameObject.CompareTag("Enemy"))
        {
           StartCoroutine(Die());
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
             StartCoroutine(Die());
        }
    }

    IEnumerator Die()
     {
        audioSource.PlayOneShot(deathSound, 0.7F);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

}
