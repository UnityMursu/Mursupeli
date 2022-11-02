using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start ()
    {
        // Gives the ice projectile a velocity
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D info)
    {
        // Find out what the ice hits
        Debug.Log(info.name);

        // Kill enemy when hit by an ice shot
        if (info.gameObject.CompareTag("Enemy"))
        {
            Destroy (info.gameObject);
            Destroy(gameObject);
        }

        // Destroy ice shot when it collides with a wall or the ground
        if (info.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        // Destroy ice shot when it collides with a trap
        if (info.gameObject.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }

    }

}
