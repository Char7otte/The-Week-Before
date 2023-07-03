using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    [Header("PlayerValues")]
    public float player_max_health = 20;
    [HideInInspector]public float player_current_health;
    [HideInInspector]public bool player_is_dead = false;
    public float player_max_stamina = 100;
    [HideInInspector]public float player_current_stamina;

    [Header("DifficultyScaling")]
    public float enemy_damage_to_player = 1;
    public float enemy_time_to_spawn = 3;
    public float difficulty_scaling_interval = 5;
    public float difficulty_scale = 1.1f;
    [HideInInspector]public float difficulty_scale_timer;

    [Header("HUD")]
    public GameObject game_over_screen;
    public int enemy_kill_count;
    public int minutes_to_survive_to_win;
    [HideInInspector]public float time_elapsed;
    [HideInInspector]public int minutes_elapsed;

    [Header("BGM")]
    public GameObject BGM;

    [Header("SFX")]
    public AudioClip player_takes_damage_sfx;
    public AudioClip player_is_dead_sfx;
    AudioSource audio_source;

    void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start() {
        player_current_health = player_max_health;
        player_current_stamina = player_max_stamina;

        audio_source = player.GetComponent<AudioSource>();
    }

    void Update() {
        if (!player_is_dead) timer();
    }

    public void player_takes_damage() {
        player_current_health -= enemy_damage_to_player;
        player_current_health = Mathf.Max(player_current_health, 0);

        audio_source.PlayOneShot(player_takes_damage_sfx);

        if (player_current_health == 0) player_has_died();
    }

    public void player_has_died() {
        player_is_dead = true;
        audio_source.PlayOneShot(player_is_dead_sfx);
        BGM.SetActive(false);
    }

    public void game_over() {
        game_over_screen.SetActive(true);
    }

    public void timer() {
        time_elapsed += Time.deltaTime;

        difficulty_scale_timer += Time.deltaTime;
        if (difficulty_scale_timer >= difficulty_scaling_interval) {
            scale_difficulty_up();
            difficulty_scale_timer = 0.0f;
        }

        if (time_elapsed >= 60.0f) {
            time_elapsed = 0.0f;
            minutes_elapsed++;
        }

        if (minutes_elapsed >= minutes_to_survive_to_win) {
            game_over();
        }
    }

    public void scale_difficulty_up() {
        enemy_damage_to_player *= difficulty_scale;
        enemy_time_to_spawn /= difficulty_scale;
        print(enemy_damage_to_player);
        print(enemy_time_to_spawn);
    }
}
