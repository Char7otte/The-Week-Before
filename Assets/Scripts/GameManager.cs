using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] playerCharacters;

    [Header("Player")]
    public static GameObject player;
    private HealthComponent playerHealthComponent;
    private DeathComponent playerDeathComponent;
    private AudioManagerComponent playerAudioManagerComponent;
    private Animator playerAnimator;

    [Header("UI")]
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField]private GameObject pauseScreen;
    [HideInInspector]public bool isPaused;
    [SerializeField]private GameObject optionsMenu;

    [Header("Trackers")]
    public int minutesToSurviveToWin = 5;
    [HideInInspector]public float secondsElapsed = 0;
    [HideInInspector]public int minutesElapsed = 0;
    [HideInInspector]public int killCount = 0;

    [Header("DifficultyScaling")]
    [SerializeField]private GameObject enemySpawnController;
    [SerializeField]private float difficultyScalingIntervalInSeconds = 5;
    public float difficultyScaleMultiplier = 0.1f;
    [HideInInspector]public int difficultyScaleMultiplierAmount = 0;
    private float difficultyScaleTimer = 0;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        AssignChosenCharacter();
        RunOnStart();
    }

    private void Update() {
        if (player == null) return;

        if (playerDeathComponent.isAlive) {
            RunTimer();
        }
        else {
            GameOver();
        }

        isPaused = pauseScreen.activeSelf;
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseScreen.SetActive(!isPaused);
            if (isPaused && optionsMenu.activeSelf) optionsMenu.SetActive(false);
        }
    }

    private void RunOnStart() {
        playerHealthComponent = player.GetComponent<HealthComponent>();
        playerDeathComponent = player.GetComponent<DeathComponent>();
        playerAudioManagerComponent = player.GetComponent<AudioManagerComponent>();
        playerAnimator = player.GetComponent<Animator>();

        secondsElapsed = 0;
        minutesElapsed = 0;
        killCount = 0;
        difficultyScaleMultiplierAmount = 0;
        difficultyScaleTimer = 0.0f;
    }

    public void AssignChosenCharacter() {
        switch (CharacterSelector.playerCharacterSelected) {
            case "pistol":
                player = playerCharacters[0];
                break;
            case "rifle":
                player = playerCharacters[1];
                break;
            default:
                player = playerCharacters[0];
                print("Error. Character selected is invalid, defaulting.");
                break;
        }

        player.SetActive(true);
    }

    public void GameOver() {
        SaveDataManager.Instance.SaveDataInt("Points", SaveDataManager.pointsCollected);

        AudioManagerMaster.Instance.Stop("BGM");
        Invoke("OpenGameOverScreen", playerAnimator.GetCurrentAnimatorStateInfo(0).length * 2);
    }

    private void OpenGameOverScreen() {
        if (playerDeathComponent.isAlive == false) killCount--; //The player dying adds to killCount, so it must be subtracted.
        gameOverScreen.SetActive(true);
    }

    public void RunTimer() {
        secondsElapsed += Time.deltaTime;
        secondsElapsed = Mathf.Min(secondsElapsed, 60.0f);
        if (secondsElapsed == 60.0f) {
            secondsElapsed = 0.0f;
            minutesElapsed++;
        }
        if (minutesElapsed >= minutesToSurviveToWin) {
            GameOver();
        }

        difficultyScaleTimer += Time.deltaTime;
        if (difficultyScaleTimer >= difficultyScalingIntervalInSeconds) {
            difficultyScaleMultiplierAmount++;
            difficultyScaleTimer = 0.0f;

            var enemySpawnControllerScript = enemySpawnController.GetComponent<EnemySpawnController>();
            enemySpawnControllerScript.timeToSpawnAfterScaling = enemySpawnControllerScript.timeToSpawn - (difficultyScaleMultiplier * difficultyScaleMultiplierAmount);
            //Damage is calculated in PlayerCollisionComponent & spawns in EnemySpawnControlelr
        }
    }
}
