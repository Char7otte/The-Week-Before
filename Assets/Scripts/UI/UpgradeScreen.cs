using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]private TextMeshProUGUI pointsText;

    [Header("DamageUpgrade")]
    [SerializeField]private Button damageUpgradeButton;
    [SerializeField]private int damageUpgradeCost;
    [SerializeField]private TextMeshProUGUI damageUpgradeCostText;
    [SerializeField]private TextMeshProUGUI damageUpgradeCountText;

    [Header("MagazineUpgrade")]
    [SerializeField]private Button magazineUpgradeButton;
    [SerializeField]private int magazineUpgradeCost;
    [SerializeField]private TextMeshProUGUI magazineUpgradeCostText;
    [SerializeField]private TextMeshProUGUI magazineUpgradeCountText;

    [Header("SpeedUpgrade")]
    [SerializeField]private Button speedUpgradeButton;
    [SerializeField]private int speedUpgradeCost;
    [SerializeField]private TextMeshProUGUI speedUpgradeCostText;
    [SerializeField]private TextMeshProUGUI speedUpgradeCountText;

    private void Update() {
        var points = SaveDataManager.pointsCollected;
        pointsText.SetText("Points Collected: " + points);

        damageUpgradeButton.interactable = SaveDataManager.pointsCollected > damageUpgradeCost;
        magazineUpgradeButton.interactable = SaveDataManager.pointsCollected > magazineUpgradeCost;
        speedUpgradeButton.interactable = SaveDataManager.pointsCollected > speedUpgradeCost;

        // if (SaveDataManager.pointsCollected < damageUpgradeCost) {
        //     damageUpgradeButton.interactable = false;
        // }
        // if (SaveDataManager.pointsCollected < magazineUpgradeCost) {
        //     magazineUpgradeButton.interactable = false;
        // }
        // if (SaveDataManager.pointsCollected < speedUpgradeCost) {
        //     speedUpgradeButton.interactable = false;
        // }

        damageUpgradeCostText.SetText("Cost: " + damageUpgradeCost);
        magazineUpgradeCostText.SetText("Cost: " + magazineUpgradeCost);
        speedUpgradeCostText.SetText("Cost: " + speedUpgradeCost);

        damageUpgradeCountText.SetText("x" + SaveDataManager.damageUpgradeCount);
        magazineUpgradeCountText.SetText("x" + SaveDataManager.magazineUpgradeCount);
        speedUpgradeCountText.SetText("x" + SaveDataManager.speedUpgradeCount);
    }

    public void PurchasedDamageUpgrade() {
        SaveDataManager.pointsCollected -= damageUpgradeCost;
        SaveDataManager.damageUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("DamageUpgradeCount", SaveDataManager.damageUpgradeCount);
    }

    public void PurchasedMagazineUpgrade() {
        SaveDataManager.pointsCollected -= magazineUpgradeCost;
        SaveDataManager.magazineUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("MagazineUpgradeCount", SaveDataManager.magazineUpgradeCount);
    }

    public void PurchasedSpeedUpgrade() {
        SaveDataManager.pointsCollected -= speedUpgradeCost;
        SaveDataManager.speedUpgradeCount++;
        SaveDataManager.Instance.SaveDataInt("SpeedUpgradeCount", SaveDataManager.speedUpgradeCount);
    }
}
