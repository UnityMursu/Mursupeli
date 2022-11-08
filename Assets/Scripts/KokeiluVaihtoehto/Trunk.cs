using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{


    
    private float waitTime;
    private float speed;
    private bool goingDown;
    private float rotationZ;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 3f;
        goingDown = true;
        rotationZ = 0f;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localEulerAngles.z > 120 && transform.localEulerAngles.z < 280)
        {
            goingDown = false;
        }

        Debug.Log(transform.localEulerAngles.z);
        if(transform.localEulerAngles.z < 340 && transform.localEulerAngles.z > 290)
        {
            goingDown = true;
        }

        if (goingDown)
        {
            transform.Rotate(0,0,Time.deltaTime * speed);
        } else 
        {
            transform.Rotate(0,0,Time.deltaTime * -speed);
        }

        if (waitTime > 0)
        {

        }

    }
}
