using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void onPlayButton()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void OpenOptions()
    {

    }
    public void CloseOptions()
    {

    }

    public void onQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
