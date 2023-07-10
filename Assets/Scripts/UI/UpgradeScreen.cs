using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI pointsCollectedText;
    [SerializeField]private TextMeshProUGUI damageUpgradeCountText;
    [SerializeField]private TextMeshProUGUI magazineUpgradeCountText;
    [SerializeField]private TextMeshProUGUI speedUpgradeCountText;

    private void Update() {
        pointsCollectedText.SetText("Points collected: " + SaveDataManager.pointsCollected);
        damageUpgradeCountText.SetText("x" + SaveDataManager.damageUpgradeCount);
        magazineUpgradeCountText.SetText("x" + SaveDataManager.magazineUpgradeCount);
        speedUpgradeCountText.SetText("x" + SaveDataManager.speedUpgradeCount);
    }

    public void Purchase(int price) {
        SaveDataManager.pointsCollected -= price;
    }

    public void AddToDamageUpgradeCount() {
        SaveDataManager.damageUpgradeCount += 1;
    }

    public void AddToMagazineUpgradeCount() {
        SaveDataManager.magazineUpgradeCount += 1;
    }

    public void AddToSpeedUpgradeCount() {
        SaveDataManager.speedUpgradeCount += 1;
    }
}
