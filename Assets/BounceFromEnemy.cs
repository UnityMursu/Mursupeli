using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFromEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerBot;
    [SerializeField]
    private float bounceForce;
    [SerializeField] private AudioSource bounceSfx;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<EnemyWeakspot>()) {
            Debug.Log("bouncy");
            bounceSfx.Play();
            playerBot.velocity = new Vector2(playerBot.velocity.x, 0f);
            playerBot.AddForce(Vector2.up * bounceForce);

        }

        if (collision.GetComponent<EnemyBouncespot>())
        {
            Debug.Log("bouncy");
            bounceSfx.Play();
            playerBot.velocity = new Vector2(playerBot.velocity.x, 0f);
            playerBot.AddForce(Vector2.up * bounceForce);

        }
    }
}
