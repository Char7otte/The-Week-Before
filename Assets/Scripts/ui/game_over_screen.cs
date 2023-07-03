using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class game_over_screen : MonoBehaviour
{
    public TMP_Text[] all_text_in_menu;
    public TMP_Text timer_text;
    public TMP_Text kill_counter_text;

    void OnEnable() {
        Time.timeScale = 0.0f;
        
        if (!GameManager.instance.player_is_dead) change_text_color_to_green();
        update_ui_text();
    }

    void OnDisable() {
        Time.timeScale = 1.0f;
    }

    void change_text_color_to_green() {
        for (int i = 0; i < all_text_in_menu.Length; i++) {
            var text = all_text_in_menu[i];
            text.color = Color.green;
        }
    }

    void update_ui_text() {
        timer_text.SetText(GameManager.instance.minutes_elapsed.ToString("00") + " : " + GameManager.instance.time_elapsed.ToString("00"));
        kill_counter_text.SetText(GameManager.instance.enemy_kill_count.ToString());
    }
}
