using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatScreenAddForce : MonoBehaviour
{
    //time stat
    [SerializeField] private Text timeStat;
    [SerializeField] private Text deathCounterText;
    [SerializeField] private GameObject player;
    private void Start()
    {
        //level time since level loaded and formats it
        timeStat.text = FormatTime(Time.timeSinceLevelLoad);
        //sets death counter from PlayerDeathScript and makes it into text
        deathCounterText.text = player.GetComponent<PlayerDeathAddForce>().deathCounter.ToString();
    }

    public void NextLevel()
    {
        //unpause time
        Time.timeScale = 1;
        //Loads next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    //format Time float into string
    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 1000 % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
