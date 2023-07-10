using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public static int pointsCollected = 0;

    [Header("Upgrades")]
    public static int damageUpgradeCount = 0;
    public static int magazineUpgradeCount = 0;
    public static int speedUpgradeCount = 0;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        LoadDataInt("Points", pointsCollected);
        LoadDataInt("DamageUpgradeCount", damageUpgradeCount);
        LoadDataInt("MagazineUpgradeCount", magazineUpgradeCount);
        LoadDataInt("SpeedUpgradeCount", speedUpgradeCount);
    }

    public void SaveDataInt(string dataName, int value) {
        PlayerPrefs.SetInt(dataName, value);
    }

    public void LoadDataInt(string dataName, int value) {
        if (!PlayerPrefs.HasKey(dataName)) {
            print("Error, no " + dataName + " saved.");
            return;
        }
        value = PlayerPrefs.GetInt(dataName);
    }
}
