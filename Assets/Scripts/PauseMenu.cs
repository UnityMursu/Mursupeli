using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    private int mainMenu = 0;

    void Update()
    {
        //Checks if Escape button has been pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //disables pausemenu and sets time back up
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void Pause()
    {
        //sets pausemenu to active, freezes time
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void LoadMenu(string sceneName)
    {
        //sets time back to normal and loads main menu
        Time.timeScale = 1;
        SaveManager.instance.DeleteSaveData();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        SaveManager.instance.DeleteSaveData();
        Application.Quit();
    }
}
