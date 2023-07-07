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

    public void EnableAndDisableGameObject(GameObject obj){
        obj.SetActive(!obj.activeSelf);
    }

    // public void OpenAndCloseCharacterSelectMenu() {
    //     characterSelectMenu.SetActive(!characterSelectMenu.activeSelf);
    // }

    // public void OpenAndCloseUpgradeSelectMenu() {
    //     upgradeSelectMenu.SetActive(!upgradeSelectMenu.activeSelf);
    // }
}
