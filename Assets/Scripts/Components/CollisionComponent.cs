using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    private GameObject player;
    private float playerGunDamage;

    private HealthComponent healthComponent;
    private Animator animator;

    private bool attacking = false;

    void Start() {
        player = GameManager.Instance.player;
        playerGunDamage = player.GetComponent<GunController>().damage;
        
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            PlayAttackAnimation();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            healthComponent.DealDamage(playerGunDamage);
        }
    }

    private void PlayAttackAnimation() {
        if (!attacking) {
            attacking = true;

            animator.SetTrigger("attack");
            Invoke("AttackingHasFinished", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void AttackingHasFinished() {
        attacking = false;
    }
}
