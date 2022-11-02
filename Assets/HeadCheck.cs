using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerBot;
    

    private void onTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<EnemyWeakspot>()) {
            Debug.Log("bouncy");
            playerBot.velocity = new Vector2(playerBot.velocity.x, 0f);
            playerBot.AddForce(Vector2.up * 300f);

        }
    }
}
