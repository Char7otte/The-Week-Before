using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public void start_game() {
        SceneManager.LoadScene("Game");
    }

    public void main_menu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit_application() {
        Application.Quit();
    }
}
