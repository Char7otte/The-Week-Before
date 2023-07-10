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
    [SerializeField]private LayerMask layerToHit;

    [Header("SpawnTimer")]
    private float timeToSpawn;
    private float spawnTimer;

    private void Start() {
        timeToSpawn = GameManager.Instance.enemyTimeToSpawn;
    }

    private void Update() {
        if (spawnTimer >= timeToSpawn) InstantiateEnemy();

        spawnTimer += Time.deltaTime;
    }

    private void InstantiateEnemy() {
        SetSpawnLocation();
        Instantiate(basicEnemyPrefab, enemySpawnLocation.position, Quaternion.identity, enemiesGroup);
        spawnTimer = 0.0f;
    }

    private void SetSpawnLocation() {
        enemySpawnAnchor.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);

        var AnchorPosition = enemySpawnAnchor.position;
        var LocationPosition = enemySpawnLocation.position;
        var direction = (AnchorPosition - LocationPosition).normalized;
        var distance = Vector3.Distance(AnchorPosition, LocationPosition);
        var ray = new Ray(AnchorPosition, direction);
        
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layerToHit)) {
            print(hit.collider.gameObject.name + "hit.");
            enemySpawnAnchor.Rotate(Vector3.right, 90);
        }
    }
}
