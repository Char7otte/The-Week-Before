using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject BGM;
    public GameObject settingsMenu;

    public bool pauseMenuActiveBool = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuActiveBool == true)
            {
                pauseMenuActiveBool = false;
                pauseMenu.SetActive(false);
                BGM.GetComponent<AudioSource>().UnPause();
                Time.timeScale = 1.0f;

                settingsMenu.SetActive(false);
            }
            else
            {
                pauseMenuActiveBool = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                BGM.GetComponent<AudioSource>().Pause();
            }
        }
    }

    void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        BGM.GetComponent<AudioSource>().Pause();
    }

    public void ExitPauseMenu()
    {
        pauseMenu.SetActive(false);
        BGM.GetComponent<AudioSource>().UnPause();
        Time.timeScale = 1.0f;
    }
}
