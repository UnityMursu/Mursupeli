using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakspot : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (collision.gameObject.CompareTag("Player"))
        {
                Destroy(transform.parent.gameObject);
        }
    }
}
