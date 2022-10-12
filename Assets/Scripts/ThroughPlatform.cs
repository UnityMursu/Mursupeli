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


        if(downPressed >= 0) {
            waitTime = 0.5f;
        }

        if(downPressed < 0){
            if(waitTime <= 0) {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;

            } else {
                waitTime -= Time.deltaTime;
            }
        }

        if(Input.GetButtonDown("Jump")) {
            effector.rotationalOffset = 0;
        }

    }

}
