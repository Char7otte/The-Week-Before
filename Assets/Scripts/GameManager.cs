using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;

    [Header("PlayerValues")]
    public float playerMaxHealth = 20;
    public float playerCurrentHealth;
    [HideInInspector]public bool isPlayerDead = false;
    public float playerMaxStamina = 100;
    [HideInInspector]public float playerCurrentStamina;

    [Header("DifficultyScaling")]
    public float enemyDamage = 1;
    public float enemyTimeToSpawn = 3;
    public float difficultyScalingIntervalInSeconds = 5;
    public float difficultyScale = 1.1f;
    [HideInInspector]public float difficultyScaleTimer;

    [Header("HUD")]
    public int enemyKillCount;
    public int minutesToSurviveToWin = 5;
    [HideInInspector]public float timeElapsed;
    [HideInInspector]public int minutesElapsed;

    [Header("Menus")]
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    public bool isPaused;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentStamina = playerMaxStamina;
    }

    private void Update() {
        if (!isPlayerDead) Timer();
        PauseMenuHandler();
    }

    public void PlayerTakesDamage() {
        playerCurrentHealth -= enemyDamage;
        playerCurrentHealth = Mathf.Max(playerCurrentHealth, 0);

        //audioSource.PlayOneShot(playerTakesDamageSFX);
        AudioManager.Instance.Play("player_hurt");

        if (playerCurrentHealth == 0) PlayerHasDied();
    }

    public void HealPlayer(float healAmount) {
        AudioManager.Instance.Play("player_heal");
        playerCurrentHealth += healAmount;
        playerCurrentHealth = Mathf.Min(playerCurrentHealth, playerMaxHealth);
    }

    public void PlayerHasDied() {
        isPlayerDead = true;
        AudioManager.Instance.Play("player_died");
        AudioManager.Instance.Stop("BGM");
    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
    }

    public void Timer() {
        timeElapsed += Time.deltaTime;

        difficultyScaleTimer += Time.deltaTime;
        if (difficultyScaleTimer >= difficultyScalingIntervalInSeconds) {
            ScaleDifficultyUp();
            difficultyScaleTimer = 0.0f;
        }

        if (timeElapsed >= 60.0f) {
            timeElapsed = 0.0f;
            minutesElapsed++;
        }

        if (minutesElapsed >= minutesToSurviveToWin) {
            GameOver();
        }
    }

    public void ScaleDifficultyUp() {
        enemyDamage *= difficultyScale;
        enemyTimeToSpawn /= difficultyScale;
    }

    public void PauseMenuHandler() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            var isPauseMenuOpen = pauseMenu.activeSelf;

            pauseMenu.SetActive(!isPauseMenuOpen);
            isPaused = !isPauseMenuOpen;
        }
    }
}
