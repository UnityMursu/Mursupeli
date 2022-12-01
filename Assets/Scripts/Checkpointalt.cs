using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointalt : MonoBehaviour
{
    private GameMaster gameMaster;
    private SpriteRenderer spriteRend;
    [SerializeField] private AudioSource checkpoint;
    public GameObject player;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = Color.red;
        
        if(SaveManager.instance.hasLoaded)
        {
            //tee if else jokaiselle movement scriptille tai muuta uusin toimimaan meressä
            gameMaster.lastCheckpointPosition = SaveManager.instance.activeSave.respawnPosition;
            PlayerMovementDJ.instance.transform.position = gameMaster.lastCheckpointPosition;
            player.GetComponent<itemCollection>().clamText.text = SaveManager.instance.activeSave.clams.ToString();
            player.GetComponent<itemCollection>().clams = SaveManager.instance.activeSave.clams;
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

            SaveManager.instance.activeSave.clams = player.GetComponent<itemCollection>().clams;
            
            SaveManager.instance.activeSave.respawnPosition = transform.position;

            SaveManager.instance.Save();

            Debug.Log("checkpoint");
            
        }
    }
}

