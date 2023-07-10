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
        LoadAllData();
    }

    public void SaveDataInt(string dataName, int value) {
        PlayerPrefs.SetInt(dataName, value);
    }

    public int LoadDataInt(string dataName) {
        return PlayerPrefs.GetInt(dataName, 0);
    }

    public void LoadAllData() {
        pointsCollected = LoadDataInt("Points");
        damageUpgradeCount = LoadDataInt("DamageUpgradeCount");
        magazineUpgradeCount = LoadDataInt("MagazineUpgradeCount");
        speedUpgradeCount = LoadDataInt("SpeedUpgradeCount"); 
    }
}
