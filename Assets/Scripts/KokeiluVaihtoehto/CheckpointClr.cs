using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointClr : MonoBehaviour
{
    private GameMaster gameMaster;
    private SpriteRenderer spriteRend;
    [SerializeField] private AudioSource checkpoint;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = Color.red;
        
        if(SaveManager.instance.hasLoaded)
        {
            gameMaster.lastCheckpointPosition = SaveManager.instance.activeSave.respawnPosition;
            PlayerMovementDJ.instance.transform.position = gameMaster.lastCheckpointPosition;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spriteRend.color != Color.green)
            {
                checkpoint.Play();
            }
            spriteRend.color = Color.green;
            gameMaster.lastCheckpointPosition = transform.position;
            
            SaveManager.instance.activeSave.respawnPosition = transform.position;

            SaveManager.instance.Save();

            Debug.Log("checkpoint");
            
        }
    }
}
