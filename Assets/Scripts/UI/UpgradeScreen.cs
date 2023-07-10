using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField]private Button damageUpgradeButton;
    [SerializeField]private int damageUpgradeCost;
    [SerializeField]private TextMeshProUGUI damageUpgradeCostText;
    [SerializeField]private TextMeshProUGUI damageUpgradeCountText;
    [SerializeField]private Button magazineUpgradeButton;
    [SerializeField]private int magazineUpgradeCost;
    [SerializeField]private TextMeshProUGUI magazineUpgradeCostText;
    [SerializeField]private TextMeshProUGUI magazineUpgradeCountText;
    [SerializeField]private Button speedUpgradeButton;
    [SerializeField]private int speedUpgradeCost;
    [SerializeField]private TextMeshProUGUI speedUpgradeCostText;
    [SerializeField]private TextMeshProUGUI speedUpgradeCountText;

    private void Update() {
        if (SaveDataManager.pointsCollected < damageUpgradeCost) {
            damageUpgradeButton.interactable = false;
        }
        if (SaveDataManager.pointsCollected < magazineUpgradeCost) {
            magazineUpgradeButton.interactable = false;
        }
        if (SaveDataManager.pointsCollected < speedUpgradeCost) {
            speedUpgradeButton.interactable = false;
        }

        damageUpgradeCostText.SetText("Cost: " + damageUpgradeCost);
        magazineUpgradeCostText.SetText("Cost: " + magazineUpgradeCost);
        speedUpgradeCostText.SetText("Cost: " + speedUpgradeCost);

        damageUpgradeCountText.SetText("x" + SaveDataManager.damageUpgradeCount);
        magazineUpgradeCountText.SetText("x" + SaveDataManager.magazineUpgradeCount);
        speedUpgradeCountText.SetText("x" + SaveDataManager.speedUpgradeCount);
    }

    

    public void purchasedDamageUpgrade(int pointsCost) {
        SaveDataManager.pointsCollected -= pointsCost;
        SaveDataManager.damageUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("DamageUpgradeCount", SaveDataManager.damageUpgradeCount);
    }

    public void purchasedMagazineUpgrade(int pointsCost) {
        SaveDataManager.pointsCollected -= pointsCost;
        SaveDataManager.magazineUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("MagazineUpgradeCount", SaveDataManager.magazineUpgradeCount);
    }

    public void purchasedSpeedUpgrade(int pointsCost) {
        SaveDataManager.pointsCollected -= pointsCost;
        SaveDataManager.speedUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("SpeedUpgradeCount", SaveDataManager.speedUpgradeCount);
    }
}
