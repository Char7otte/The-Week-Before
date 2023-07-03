using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_collisions : MonoBehaviour
{
    GameObject player;
    Animator animator;

    bool attacking = false;

    public GameObject medkit_prefab;
    public int medkit_drop_chance;

    void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            attack();
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            attack();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            var gun_script = player.GetComponent<rifle_controller>();
            var health_component = GetComponent<health_component>();
            health_component.deal_damage(gun_script.damage);

            if (health_component.is_dead_signal()) enemy_has_died();
        }
    }

    void attack() {
        if (!attacking) {
            attacking = true;

            animator.SetTrigger("attack");
            Invoke("attacking_finished", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void attacking_finished() {
        attacking = false;
    }

    void enemy_has_died() {
        GetComponent<enemy_movement_controller>().enabled = false;
        GetComponent<Collider>().enabled = false;

        spawn_med_kit_check();
        play_death_animation();

        GameManager.instance.enemy_kill_count++;
    } 

    void spawn_med_kit_check() {
        var random_int = Random.Range(0, 100);
        print(random_int);
        if (random_int < medkit_drop_chance) Instantiate(medkit_prefab, transform.position, Quaternion.identity);
    }

    void play_death_animation() {
        animator.SetTrigger("dead");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length * 2);
    }
}
