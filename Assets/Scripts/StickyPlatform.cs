using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    //makes the player stay on the platform (etc.) while it moves
    private Rigidbody2D _bodies;
    private bool _playerAttached;

    private void FixedUpdate()
    {
        /*if(_playerAttached)
        {
            _bodies.velocity = GetComponent<Rigidbody2D>().velocity;
         }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            _playerAttached = true;
            _bodies = GetComponentInChildren<Rigidbody2D>();
        }
    }

    //player is able to move away from the platform, platform needs layer jumbable ground
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerAttached = false;
            collision.gameObject.transform.SetParent(null);
        }
    }
}
