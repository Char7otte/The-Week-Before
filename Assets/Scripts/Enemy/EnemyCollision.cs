using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private GameObject player;
    private Animator animator;

    private bool attacking = false;

    public GameObject medkitPrefab;
    public int medkitDropChance;

    void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Attack();
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            var gunController = player.GetComponent<GunController>();
            var healthComponent = GetComponent<HealthComponent>();
            healthComponent.DealDamage(gunController.damage);

            if (healthComponent.IsDeadSignal()) EnemyHasDied();
        }
    }

    void Attack() {
        if (!attacking) {
            attacking = true;

            animator.SetTrigger("attack");
            Invoke("AttackingHasFinished", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void AttackingHasFinished() {
        attacking = false;
    }

    void EnemyHasDied() {
        GetComponent<EnemyMovementController>().enabled = false;
        GetComponent<Collider>().enabled = false;

        RollForChanceToSpawnMedkit();
        PlayDeathAnimation();

        GameManager.Instance.enemyKillCount++;
    } 

    void RollForChanceToSpawnMedkit() {
        var randomInt = Random.Range(0, 100);
        if (randomInt < medkitDropChance) Instantiate(medkitPrefab, transform.position, Quaternion.identity);
    }

    void PlayDeathAnimation() {
        animator.SetTrigger("dead");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length * 2);
    }
}
