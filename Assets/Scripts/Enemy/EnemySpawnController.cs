using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Header("InstantiatingEnemies")]
    [SerializeField]private Transform enemiesGroup;
    [SerializeField]private GameObject basicEnemyPrefab;
    [SerializeField]private LayerMask layerToHit;
    private Transform enemySpawnAnchor; //idk what to call this lol
    private Transform enemySpawnLocation;

    [Header("SpawnTimer")]
    public float timeToSpawn;
    [HideInInspector]public float timeToSpawnAfterScaling;
    private float spawnTimer;

    private void Start() {
        timeToSpawnAfterScaling = timeToSpawn;
        var player = GameManager.player;
        enemySpawnAnchor = player.transform.GetChild(0).transform;
        enemySpawnLocation = enemySpawnAnchor.GetChild(0).transform;
    }

    private void Update() {
        if (spawnTimer >= timeToSpawnAfterScaling) InstantiateEnemy();

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
