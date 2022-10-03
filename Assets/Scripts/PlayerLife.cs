using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (player.transform.position.y <= enemy.transform.position.y)
            {
                Die();
            }
        }
    }

    private void Die()
     {
         rigidBody.bodyType = RigidbodyType2D.Static;
        //animation
        RestartLevel();
     }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}