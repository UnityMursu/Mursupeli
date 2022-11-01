using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;
    private float downPressed;

    void Start() {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update(){
        
        
        downPressed = Input.GetAxisRaw("Vertical");

        if(downPressed >= 0 ) {
            //waitTime = 0.0f;
            waitTime -= Time.deltaTime;
        }

        if(downPressed < 0 && Input.GetButtonDown("Jump")){
            Debug.Log("jumpdown");
            //if(waitTime <= 0) {
                effector.rotationalOffset = 180f;
                waitTime = 0.15f;

            //} else {
            //}
        }

        if(/*Input.GetButtonDown("Jump") && */downPressed >= 0 && waitTime <= 0.0f) {
            effector.rotationalOffset = 0;
        }

    }

}
