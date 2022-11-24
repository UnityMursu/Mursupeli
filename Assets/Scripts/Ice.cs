using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float speed = 20f;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public bool destroyed;
    [SerializeField] public AudioSource impact;

    // Start is called before the first frame update
    void Start ()
    {
        // Gives the ice projectile a velocity
        sprite = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        rb.velocity = transform.right * speed;
        destroyed = false;
    }

    void OnTriggerEnter2D (Collider2D info)
    {
        // Find out what the ice hits
        Debug.Log(info.name);

        // Kill enemy when hit by an ice shot
        if (info.gameObject.CompareTag("Enemy"))
        {
            impact.Play();
            Destroy (info.gameObject);
            sprite.enabled = false;
            cc.enabled = false;
            Destroy(gameObject, impact.clip.length);
        }

        // Destroy ice shot when it collides with a wall or the ground
        if (info.gameObject.CompareTag("Ground"))
        {
            impact.Play();
            sprite.enabled = false;
            cc.enabled = false;
            Destroy(gameObject, impact.clip.length);
        }

        // Destroy ice shot when it collides with a trap
        if (info.gameObject.CompareTag("Trap"))
        {
            impact.Play();
            sprite.enabled = false;
            cc.enabled = false;
            Destroy(gameObject, impact.clip.length);
        }

        // Destroy ice shot when it collides with a Pit
        if (info.gameObject.CompareTag("Pit"))
        {
            impact.Play();
            sprite.enabled = false;
            cc.enabled = false;
            Destroy(gameObject, impact.clip.length);
        }

    }

    void OnDestroy()
    {
        destroyed = true;
    }

}
