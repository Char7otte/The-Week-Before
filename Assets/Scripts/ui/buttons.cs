using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField]private GameObject optionsMenu;

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitApplication() {
        Application.Quit();
    }

    public void OpenAndCloseOptionsMenu(){
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }
}
