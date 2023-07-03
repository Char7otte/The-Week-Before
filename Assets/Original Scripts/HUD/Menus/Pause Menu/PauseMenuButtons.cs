using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject settingsMenu;

    public PauseMenu script;

    public void ResumeGame()
    {
        script.pauseMenuActiveBool= false;
        settingsMenu.SetActive(false);
        script.ExitPauseMenu();
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
