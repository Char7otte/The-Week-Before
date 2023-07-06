using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveGameUI : MonoBehaviour
{
    [SerializeField]private TMP_Text timerText;
    [SerializeField]private TMP_Text healthText;
    [SerializeField]private Image healthbarImage;
    [SerializeField]private TMP_Text staminaText;
    [SerializeField]private Image staminabarImage;
    [SerializeField]private TMP_Text ammoText;
    //public TMP_Text ammoCountText; This is managed by player's GunController

    private void Update() {
        timerText.SetText(GameManager.Instance.minutesElapsed.ToString("00") + " : " + GameManager.Instance.timeElapsed.ToString("00"));

        float currentHealth = GameManager.Instance.playerCurrentHealth;
        float maxHealth = GameManager.Instance.playerMaxHealth;
        healthText.SetText("Health: " + (currentHealth * 5).ToString("0"));
        healthbarImage.fillAmount = currentHealth / maxHealth;

        float currentStamina = GameManager.Instance.playerCurrentStamina;
        float maxStamina =GameManager.Instance.playerMaxStamina;
        staminaText.SetText("Stamina:" + currentStamina.ToString("0"));
        staminabarImage.fillAmount = currentStamina / maxStamina;
    }
}
