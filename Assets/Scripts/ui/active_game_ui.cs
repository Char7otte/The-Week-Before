using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class active_game_ui : MonoBehaviour
{
    public TMP_Text timer_text;
    public TMP_Text health_text;
    public Image healthbar_image;
    public TMP_Text stamina_text;
    public Image staminabar_image;
    //public TMP_Text ammo_count_text; This is managed by player's rifle_controller

    void Update() {
        timer_text.SetText(GameManager.instance.minutes_elapsed.ToString("00") + " : " + GameManager.instance.time_elapsed.ToString("00"));

        float current_health = GameManager.instance.player_current_health;
        float max_health = GameManager.instance.player_max_health;
        health_text.SetText("Health: " + (current_health * 5).ToString("0"));
        healthbar_image.fillAmount = current_health / max_health;

        float current_stamina = GameManager.instance.player_current_stamina;
        float max_stamina =GameManager.instance.player_max_stamina;
        stamina_text.SetText("Stamina:" + current_stamina.ToString("0"));
        staminabar_image.fillAmount = current_stamina / max_stamina;
    }
}
