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
        player = GameObject.FindWithTag("Player");
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
        
        if(collision.gameObject.tag == "Player")
        {
        Debug.Log("osuma");
        player.GetComponent<PlayerMovementDJ>().blindTime = 3f;
        Destroy();
        }
     }
      
     
    
}