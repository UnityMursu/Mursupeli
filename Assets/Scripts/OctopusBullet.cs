using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBullet : MonoBehaviour
{
   public float destroyTime;
   public float blindTime;
   public GameObject objectToDestroy;


    void Start()
    {
        StartCoroutine(CountDownTimer());
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
    
    void Destroy()
    {
        Destroy(gameObject);
    }
}
