using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI killCounterText;

    private void OnEnable() {
        Time.timeScale = 0.0f;

        if (GameManager.player.GetComponent<DeathComponent>().isAlive) ChangeTextColorToGreen();
        UpdateUIText();
    }

    private void OnDisable() {
        Time.timeScale = 1.0f;
    }

    private void ChangeTextColorToGreen() {
         foreach (Transform child in transform) {
            if (child.GetComponent<TextMeshProUGUI>() != null) {
                child.GetComponent<TextMeshProUGUI>().color = new Color(0, 255, 0, 255);
            }
        }
    }

    private void UpdateUIText() {
        timerText.SetText(GameManager.Instance.minutesElapsed.ToString("00") + " : " + GameManager.Instance.secondsElapsed.ToString("00"));
        killCounterText.SetText(GameManager.Instance.killCount.ToString());
    }
}
