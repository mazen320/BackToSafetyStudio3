using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menus")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;

    public static bool GameisStopped = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown("m"))
        {
            if (GameisStopped)
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ShowObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ShowObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }

    public void removeObjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
