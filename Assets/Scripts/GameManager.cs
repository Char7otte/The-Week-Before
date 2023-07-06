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
    [HideInInspector]public float playerCurrentHealth;
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

    [Header("BGM")]
    public GameObject BGM;

    [Header("SFX")]
    public AudioClip playerTakesDamageSFX;
    public AudioClip PlayerDeadSFX;
    private AudioSource audioSource;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentStamina = playerMaxStamina;

        audioSource = player.GetComponent<AudioSource>();
    }

    private void Update() {
        if (!isPlayerDead) timer();
        if (Input.GetKeyDown(KeyCode.Escape)) enablePauseMenu();
    }

    public void playerTakesDamage() {
        playerCurrentHealth -= enemyDamage;
        playerCurrentHealth = Mathf.Max(playerCurrentHealth, 0);

        audioSource.PlayOneShot(playerTakesDamageSFX);

        if (playerCurrentHealth == 0) playerHasDied();
    }

    public void playerHasDied() {
        isPlayerDead = true;
        audioSource.PlayOneShot(PlayerDeadSFX);
        BGM.SetActive(false);
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
    }

    public void timer() {
        timeElapsed += Time.deltaTime;

        difficultyScaleTimer += Time.deltaTime;
        if (difficultyScaleTimer >= difficultyScalingIntervalInSeconds) {
            scaleDifficultyUp();
            difficultyScaleTimer = 0.0f;
        }

        if (timeElapsed >= 60.0f) {
            timeElapsed = 0.0f;
            minutesElapsed++;
        }

        if (minutesElapsed >= minutesToSurviveToWin) {
            gameOver();
        }
    }

    public void scaleDifficultyUp() {
        enemyDamage *= difficultyScale;
        enemyTimeToSpawn /= difficultyScale;
    }

    public void enablePauseMenu() {
        pauseMenu.SetActive(true);
    }
}
