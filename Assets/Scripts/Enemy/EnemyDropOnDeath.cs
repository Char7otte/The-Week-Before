using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdditionalDeathComponent : MonoBehaviour
{
    [SerializeField]private GameObject medkitPrefab;
    [SerializeField]private int medkitPrefabDropChance;

    [SerializeField]private GameObject pointsPrefab;

    public void CallUponDeath() {
        var randomInt = Random.Range(0, 100);
        if (randomInt < medkitPrefabDropChance) Instantiate(medkitPrefab, transform.position, Quaternion.identity);

        Instantiate(pointsPrefab, transform.position, Quaternion.identity);
    }
}
