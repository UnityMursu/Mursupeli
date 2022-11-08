using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDeath : MonoBehaviour
{
    private GameObject player;
    PlayerLife PlayerLife;
    
    //kills player if they are below the falldeathpoint y-axis
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player.transform.position.y < this.gameObject.transform.position.y)
        {
            PlayerLife.Die();
            PlayerLife.LoadLastCheckpoint();

        }
    }
}
