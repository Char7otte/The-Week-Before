using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("EnemyMovement")]
    [SerializeField]private float movementSpeed = 1.0f;

    [Header("Player")]
    private GameObject player;

    private void Start() {
        player = GameManager.player;
    }

    private void Update() {
        if (player == null) return;
        transform.LookAt(player.transform.position);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
