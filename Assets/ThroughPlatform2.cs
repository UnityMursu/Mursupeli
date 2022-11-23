using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform2 : MonoBehaviour
{
    private PlatformEffector2D effector;
    private Collider2D collider;
    private bool onPlatform;
    private float downPressed;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {


        downPressed = Input.GetAxisRaw("Vertical");

        if (downPressed < 0 && Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumpdown");
            //if(waitTime <= 0) {
            collider.enabled = false;
            StartCoroutine(routine: EnableCollider());

            //} else {
            //}
        }

    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
}
