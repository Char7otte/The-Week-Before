using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timeToSpawn;
    float timer;
    public float minSpawnTime = 1f, maxSpawnTime = 4f;

    public float radius = 20f;

    public GameObject enemyPrefab;

    void Start()
    {
        timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        GetComponent<SphereCollider>().enabled = false;
    }
    void Update()
    {
        timer += Time.deltaTime;

        var x = Random.Range(-29f, 29f);
        var z = Random.Range(-29f, 29f);


        if (timer >= timeToSpawn)
        {
            timer = 0;
            timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);

            var randomPos = new Vector3 (x,1,z);
            Instantiate(enemyPrefab, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}
