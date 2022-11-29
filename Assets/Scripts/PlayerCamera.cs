using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float waitTime;
    private float downPressed;
    private Vector3 scrollDistance;
    private float scrollTime;
    private bool scroll;


    void Start ()
    {
        waitTime = 1f;
        scrollDistance = new Vector3(0.0f, 5.0f, 0.0f);
        scrollTime = 4f;
        scroll = false;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        downPressed = Input.GetAxisRaw("Vertical");
        transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position


        // pressing down will count waittime
        if (downPressed < 0) {
            waitTime -= Time.deltaTime;
            
        }
        // after waittime is 0 or less camera will go down from players position (so you can try to peak downwards) Testing to be made to make it smooth scrolling
        if (waitTime <= 0){ 
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3 (startPosition.x, startPosition.y - scrollDistance.y, startPosition.z);
            float time = 0;
            //StartCoroutine scrollCameraDown(scrollDistance)
            while (time < scrollTime) {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, time/scrollTime);
                scroll = true;
            }
        }
        // if camera is positioned to look down but not pressing down camera will reset to original position
        if (scroll = true && downPressed >= 0) {
            transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z);
            scroll = false;
            waitTime = 1f;
        }
    }

    /*IEnumerator scrollCameraDown(scrollDistance) {
        startPosition = transform.position.y;
        endPosition = transform.position.y + scrollDistance;

    }*/
}
