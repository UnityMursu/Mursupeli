using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBullet : MonoBehaviour
{
   public float destroyTime;
   public float blindTime;
   public GameObject objectToDestroy;
   public GameObject BlackScreen;


    void Start()
    {
        StartCoroutine(CountDownTimer());
        BlackScreen.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(destroyTime);
        Destroy();
    }
    
       IEnumerator BlindTimer()
     {
        Debug.Log("hmm");
        yield return new WaitForSeconds(blindTime);
        Debug.Log("aika");
        BlackScreen.gameObject.SetActive(false);
     }
    
    void Destroy()
    {
        Destroy(objectToDestroy.gameObject);
    }
    
    void BlindPlayer()
    {
    Debug.Log("blinded");
    BlackScreen.gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision) 
     {
        
     //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if(collision.gameObject.tag == "Player")
        {
        Destroy();
        Debug.Log("osuma");
        BlindPlayer();
        StartCoroutine(BlindTimer());
        }
     }
      
     
    
}