using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointClr : MonoBehaviour
{
    private GameMaster gameMaster;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = Color.red;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRend.color = Color.green;
            gameMaster.lastCheckpointPosition = transform.position;
            Debug.Log("checkpoint");
        }
    }
}
