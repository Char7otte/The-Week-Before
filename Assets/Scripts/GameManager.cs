using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] playerCharacters;

    [Header("Player")]
    [HideInInspector]public GameObject player;
    private HealthComponent playerHealthComponent;
    private DeathComponent playerDeathComponent;
    private AudioManagerComponent playerAudioManagerComponent;
    private Animator playerAnimator;

    [Header("UI")]
    [SerializeField]private GameObject gameOverScreen;
    [HideInInspector]public bool playerIsDead = false;
    [SerializeField]private GameObject pauseScreen;
    [HideInInspector]public bool isPaused;
    [SerializeField]private GameObject optionsMenu;

    [Header("Trackers")]
    public int minutesToSurviveToWin = 5;
    [HideInInspector]public float secondsElapsed;
    [HideInInspector]public int minutesElapsed;
    [HideInInspector]public int killCount;

    [Header("DifficultyScaling")]
    public float enemyTimeToSpawn = 3;
    public float difficultyScalingIntervalInSeconds = 5;
    public float difficultyScale = 1.1f;
    [HideInInspector]public float difficultyScaleTimer;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        AssignChosenCharacter();

        playerHealthComponent = player.GetComponent<HealthComponent>();
        playerDeathComponent = player.GetComponent<DeathComponent>();
        playerAudioManagerComponent = player.GetComponent<AudioManagerComponent>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Update() {
        if (playerDeathComponent.isAlive) {
            RunTimer();
        }
        else {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenu.activeSelf) {
            isPaused = pauseScreen.activeSelf;
            pauseScreen.SetActive(!isPaused);
        }
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

        killCount--; //DeathComponent will add the player's death, so it has to be subtracted here.
        AudioManagerMaster.Instance.Stop("BGM");
        Invoke("OpenGameOverScreen", playerAnimator.GetCurrentAnimatorStateInfo(0).length * 2);
    }

    private void OpenGameOverScreen() {
        gameOverScreen.SetActive(true);
    }

    public void RunTimer() {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed >= 60.0f) {
            secondsElapsed = 0.0f;
            minutesElapsed++;
        }
        if (minutesElapsed >= minutesToSurviveToWin) {
            GameOver();
        }

        difficultyScaleTimer += Time.deltaTime;
        if (difficultyScaleTimer >= difficultyScalingIntervalInSeconds) {
            //ScaleDifficultyUp();
            difficultyScaleTimer = 0.0f;
        }
    }

    //public void ScaleDifficultyUp() {
    //     enemyDamage *= difficultyScale;
    //     enemyTimeToSpawn /= difficultyScale;
    // }
}
