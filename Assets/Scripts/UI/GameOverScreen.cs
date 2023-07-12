using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI killCounterText;

    [SerializeField]private Transform anchor;

    private void OnEnable() {
        Time.timeScale = 0.0f;

        if (GameManager.player.GetComponent<DeathComponent>().isAlive) ChangeTextColorToGreen();
        UpdateUIText();
    }

    private void OnDisable() {
        Time.timeScale = 1.0f;
    }

    public void ChangeTextColorToGreen() {
         foreach (Transform child in anchor) {
            if (child.GetComponent<TextMeshProUGUI>() != null) {
                child.GetComponent<TextMeshProUGUI>().color = new Color(0, 255, 0, 255);
            }
        }
    }

    private void UpdateUIText() {
        var minutesElapsed = GameManager.Instance.minutesElapsed;
        var secondsElapsed = GameManager.Instance.secondsElapsed;
        timerText.SetText(minutesElapsed.ToString("00") + " : " + secondsElapsed.ToString("00"));

        killCounterText.SetText(GameManager.Instance.killCount.ToString());
    }
}
