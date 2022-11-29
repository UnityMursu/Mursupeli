using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastLevelGoal : MonoBehaviour
{

    [SerializeField] private AudioSource goalSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goalSfx.Play();
            //pause time
            Time.timeScale = 0;
            //loads endscreen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
