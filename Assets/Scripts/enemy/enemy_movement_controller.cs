using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement_controller : MonoBehaviour
{
    [Header("EnemyMovement")]
    public float movement_speed = 1.0f;

    [Header("Player")]
    public GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }


    void Update() {
        transform.LookAt(player.transform.position);
        transform.position += transform.forward * movement_speed * Time.deltaTime;
    }
}
