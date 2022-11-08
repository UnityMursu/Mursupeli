using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private GameObject player;
    private GameMaster gameMaster;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayerAbove = player.transform.position.y >= collision.otherCollider.transform.position.y;

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (isPlayerAbove)
                {
                    Destroy(collision.otherCollider);
                }
                else {
                    Die();
                }
                break;
            case "Unkillable":
                Die();
                break;
        }
    }

    public void Die()
     {
        //animation
        LoadLastCheckpoint();
     }

    public void LoadLastCheckpoint()
    {
        player.transform.position = gameMaster.lastCheckpointPosition;
    }

}