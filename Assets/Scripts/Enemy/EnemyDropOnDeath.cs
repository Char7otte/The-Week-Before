using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdditionalDeathComponent : MonoBehaviour
{
    [SerializeField]private GameObject medkitPrefab;
    [SerializeField]private int medkitPrefabDropChance;

    [SerializeField]private GameObject pointsPrefab;

    private HealthComponent healthComponent;
    private bool called;

    private void Start() {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Update() {
        if (healthComponent.currentHealth <= 0 && !called) {
            called = true;
            var randomInt = Random.Range(0, 100);
            if (randomInt < medkitPrefabDropChance) Instantiate(medkitPrefab, transform.position, Quaternion.identity);
            else Instantiate(pointsPrefab, transform.position, Quaternion.identity);
        }
    }
}
