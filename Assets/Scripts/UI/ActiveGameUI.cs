using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveGameUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;

    
    [SerializeField]private Image healthbarImage;
    [SerializeField]private TextMeshProUGUI healthText;
    private HealthComponent healthComponent;

    [SerializeField]private Image staminabarImage;
    [SerializeField]private TextMeshProUGUI staminaText;
    private PlayerMovementController playerMovementController; //Yes, there is a StaminaComponent script. Might update this & that in the future.

    [SerializeField]private TextMeshProUGUI ammoText;
    private GunController gunController;

    private void Start() {
        gunController = GameManager.Instance.player.GetComponent<GunController>();
        healthComponent = GameManager.Instance.player.GetComponent<HealthComponent>();
        playerMovementController = GameManager.Instance.player.GetComponent<PlayerMovementController>();
    }

    private void Update() {
        var minutesElapsed = GameManager.Instance.minutesElapsed;
        var secondsElapsed = GameManager.Instance.secondsElapsed;
        timerText.SetText(minutesElapsed.ToString("00" + " : " + secondsElapsed.ToString("00")));

        var currentHealth = healthComponent.currentHealth;
        var maxHealth = healthComponent.maxHealth;
        healthText.SetText("Health: " + (currentHealth * 5).ToString("0"));
        healthbarImage.fillAmount = currentHealth / maxHealth;

        var currentStamina = playerMovementController.currentStamina;
        var maxStamina = playerMovementController.maxStamina;
        staminaText.SetText("Stamina:" + currentStamina.ToString("0"));
        staminabarImage.fillAmount = currentStamina / maxStamina;
    }
}
