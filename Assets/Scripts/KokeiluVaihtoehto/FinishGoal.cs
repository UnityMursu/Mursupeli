using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGoal : MonoBehaviour
{
    [SerializeField] private AudioSource goalSfx;

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
            Invoke("CompleteLevel", 3);
        }
    }

    private void CompleteLevel() 
    {
        //reloads current
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //moves to next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
