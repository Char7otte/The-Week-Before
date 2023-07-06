using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Header("InstantiatingEnemies")]
    [SerializeField]private Transform enemySpawnAnchor; //idk what to call this lol
    [SerializeField]private Transform enemySpawnLocation;
    [SerializeField]private Transform enemiesGroup;
    [SerializeField]private GameObject basicEnemyPrefab;

    [Header("SpawnTimer")]
    private float timeToSpawn;
    private float spawnTimer;

    private void Start() {
        timeToSpawn = GameManager.Instance.enemyTimeToSpawn;
    }

    private void Update() {
        if (spawnTimer >= timeToSpawn) instantiateEnemy();

        spawnTimer += Time.deltaTime;
    }

    private void instantiateEnemy() {
        setSpawnLocation();
        Instantiate(basicEnemyPrefab, enemySpawnLocation.position, Quaternion.identity, enemiesGroup);
        spawnTimer = 0.0f;
    }

    private void setSpawnLocation() {
        enemySpawnAnchor.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
    }
}
