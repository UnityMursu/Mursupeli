using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.transform.position.y >= enemy.transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
