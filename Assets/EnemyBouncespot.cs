using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBouncespot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");


        if (collision.GetComponent<BounceFromEnemy>())
        {
            Debug.Log("no die");
        }
    }
}
