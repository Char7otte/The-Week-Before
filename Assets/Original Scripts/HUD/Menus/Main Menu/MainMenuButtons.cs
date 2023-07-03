using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject shopMenu;

    public GameObject BGM;

    public void StartGame()
    {
        SceneManager.LoadScene("Game Level");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
