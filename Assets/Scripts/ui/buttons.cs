using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private AudioManagerComponent audioManagerComponent;

    private void Start() {
        audioManagerComponent = GetComponent<AudioManagerComponent>();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitApplication() {
        Application.Quit();
    }

    public void EnableAndDisableGameObject(GameObject obj){
        obj.SetActive(!obj.activeSelf);
        PlayButtonClick();
    }

    public void PlayButtonClick() {
        audioManagerComponent.Play("button_click");
    }
}
