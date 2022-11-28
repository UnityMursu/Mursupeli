using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OctopusBullet : MonoBehaviour
{

    public GameObject objectToDestroy;
    public GameObject player;



    void Start()
    {
        StartCoroutine(CountDownTimer());
        player = GameObject.Find("PlayerNoCam");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* void OnCollisionEnter2D(Collision2D col)
    {
        Destroy();
    } */

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy();
    }
    
  
    
    void Destroy()
    {
        Destroy(objectToDestroy.gameObject);
    }
    

    void OnCollisionEnter2D(Collision2D collision) 
     {
        
     //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if(collision.gameObject.tag == "Player")
        {
        Debug.Log("osuma");
        player.GetComponent<PlayerMovementDJ>().blindTime = 3f;
        Destroy();
        }
     }
      
     
    
}