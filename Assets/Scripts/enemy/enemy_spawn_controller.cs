using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawn_controller : MonoBehaviour
{
    [Header("InstantiatingEnemies")]
    public Transform enemy_spawn_anchor; //idk what to call this lol
    public Transform enemy_spawn_location;
    public Transform parent_of_enemies;
    public GameObject basic_enemy_prefab;

    [Header("SpawnTimer")]
    float time_to_spawn;
    float spawn_timer;

    void Start() {
        time_to_spawn = GameManager.instance.enemy_time_to_spawn;
    }

    void Update() {
        if (spawn_timer >= time_to_spawn) instantiate_enemy();

        spawn_timer += Time.deltaTime;
    }

    void instantiate_enemy() {
        set_spawn_location();
        Instantiate(basic_enemy_prefab, enemy_spawn_location.position, Quaternion.identity, parent_of_enemies);
        spawn_timer = 0.0f;
    }

    void set_spawn_location() {
        enemy_spawn_anchor.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
    }
}
