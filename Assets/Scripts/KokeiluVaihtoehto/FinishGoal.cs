using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGoal : MonoBehaviour
{
    [SerializeField] private AudioSource goalSfx;
    [SerializeField] private GameObject stats;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            /*without delay 
             CompeleLevel()*/
            goalSfx.Play();
            Time.timeScale = 0;
            //shows stat screen - needs CanvasRenderer Stats from Canvas StatScreen
            stats.SetActive(true);
        }
    }
}
