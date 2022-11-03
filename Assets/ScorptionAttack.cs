using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorptionAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public float attackTimer = 2f;
    private GameObject test;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Collider2D[] hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        /*foreach(Collider2D player in hitPlayer)
        {
            Debug.Log("We hit player");
        }*/
        Debug.Log("Player!!! Die");
        /*test = transform.Find("Test").gameObject;
        if(test != null)
            {
                Destroy(transform.Find("Test").gameObject);
            }
        */

    }
}
