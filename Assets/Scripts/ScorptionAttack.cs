using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorptionAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 2f;
    public LayerMask playerLayer;
    PlayerDeath playerDeath;

    
    void Start()
    {
        playerDeath = GameObject.FindWithTag("Player").GetComponent<PlayerDeath>();
    }

    //When Attack() is called, it creates temporary circle to check if player is in range. If player in range it calls Die() from playerdeath script
    public void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        foreach(Collider2D player in hitPlayer)
        {
            playerDeath.Die();
        }
    }
    // Show's the attacking area in unity engine as a circle gizmo for easier configuration
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
