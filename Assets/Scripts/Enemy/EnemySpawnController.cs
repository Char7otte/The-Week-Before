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
    private float distanceBetweenSpawnPointAndAnchor;
    [SerializeField]private LayerMask layersToHit;

    [Header("SpawnTimer")]
    private float timeToSpawn;
    private float spawnTimer;

    private void Start() {
        timeToSpawn = GameManager.Instance.enemyTimeToSpawn;
        distanceBetweenSpawnPointAndAnchor = Vector3.Distance(enemySpawnLocation.position, enemySpawnAnchor.position);
    }

    private void Update() {
        if (spawnTimer >= timeToSpawn) InstantiateEnemy();

        spawnTimer += Time.deltaTime;
    }

    private void InstantiateEnemy() {
        enemySpawnAnchor.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);

        CheckForOutOfBoundsSpawnLocation();

        Instantiate(basicEnemyPrefab, enemySpawnLocation.position, Quaternion.identity, enemiesGroup);
        spawnTimer = 0.0f;
    }

    private void CheckForOutOfBoundsSpawnLocation() {
        var spawnAnchorPosition = enemySpawnAnchor.position;
        var spawnLocationPosition = enemySpawnLocation.position;
        var direction = (spawnLocationPosition - spawnAnchorPosition).normalized;
        var distance = Vector3.Distance(spawnAnchorPosition, spawnLocationPosition);
        
        Ray ray = new Ray(spawnAnchorPosition, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layersToHit)) {
            print(hit.collider.gameObject.name + "was hit.");
            enemySpawnAnchor.Rotate(Vector3.right, 90, 0);
            CheckForOutOfBoundsSpawnLocation();
        }
    }
}
