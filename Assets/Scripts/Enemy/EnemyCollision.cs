// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyCollision : MonoBehaviour
// {
//     void EnemyHasDied() {
//         GetComponent<EnemyMovementController>().enabled = false;
//         GetComponent<Collider>().enabled = false;

//         RollForChanceToSpawnMedkit();
//         PlayDeathAnimation();

//         GameManager.Instance.enemyKillCount++;
//     } 

//     void RollForChanceToSpawnMedkit() {
//         var randomInt = Random.Range(0, 100);
//         if (randomInt < medkitDropChance) Instantiate(medkitPrefab, transform.position, Quaternion.identity);
//     }

//     void PlayDeathAnimation() {
//         animator.SetTrigger("dead");
//         Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length * 2);
//     }
// }
