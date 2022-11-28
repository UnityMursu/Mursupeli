using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    private bool isSlippery = false;
    [SerializeField] private Rigidbody2D player;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSlippery = true;
        player.velocity = new Vector2(player.velocity.x * 4f, player.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSlippery = false;
    }
}
