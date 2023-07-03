using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game Level");
        Time.timeScale = 1.0f;
    }

    public void ExitBacktoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
