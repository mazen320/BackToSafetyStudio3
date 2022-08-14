using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menus")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject canvasParent;

    private bool GameisStopped = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisStopped)
            {
                Resume();
                canvasParent.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Pause();
               canvasParent.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisStopped = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu 1");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
    }
    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }

}
