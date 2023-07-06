using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void startGame() {
        SceneManager.LoadScene("Game");
    }

    public void goToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitApplication() {
        Application.Quit();
    }

    public void openOptionsMenu(){
        var optionsMenu = GameObject.Find("OptionsScreen");
        optionsMenu.SetActive(true);
    }
<<<<<<< Updated upstream
=======

    public void playSFX() {
        AudioManager.Instance.Play("sfx");
    }

    public void playMusic() {
        AudioManager.Instance.Play("music");
    }
>>>>>>> Stashed changes
}
