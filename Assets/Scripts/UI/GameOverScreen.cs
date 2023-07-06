using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]private TMP_Text[] allTextInMenu;
    [SerializeField]private TMP_Text timerText;
    [SerializeField]private TMP_Text killCounterText;

    private void OnEnable() {
        Time.timeScale = 0.0f;
        
        if (!GameManager.Instance.isPlayerDead) changeTextColorToGreen();
        updateUIText();
    }

    private void OnDisable() {
        Time.timeScale = 1.0f;
    }

    private void changeTextColorToGreen() {
        for (int i = 0; i < allTextInMenu.Length; i++) {
            var text = allTextInMenu[i];
            text.color = Color.green;
        }
    }

    private void updateUIText() {
        timerText.SetText(GameManager.Instance.minutesElapsed.ToString("00") + " : " + GameManager.Instance.timeElapsed.ToString("00"));
        killCounterText.SetText(GameManager.Instance.enemyKillCount.ToString());
    }
}
