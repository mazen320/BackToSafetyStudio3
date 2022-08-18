using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject optionsScreen;

    public void onPlayButton()
    {
        SceneManager.LoadScene("Main Main");
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void onQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
