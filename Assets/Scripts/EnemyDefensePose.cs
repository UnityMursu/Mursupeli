using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefensePose : MonoBehaviour
{
    private Animator animator;
    //private Rigidbody2D rigidBody;
    //rigid body rikkoo k‰vely skripti‰, j‰‰ nykim‰‰n liikkumispisteisiin

    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //voisi lis‰t‰ jotain, mill‰ lopettaa liikkumisen
            //alla oleva ei toimi
            //rigidBody.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("defense");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //rigidBody.bodyType = RigidbodyType2D.Dynamic;
            animator.SetTrigger("walk");
        }
    }
}
