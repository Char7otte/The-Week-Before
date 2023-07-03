using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_collisions : MonoBehaviour
{  
    [Header("InvincibilityFrames")]
    public float invincibility_frames_amount = 1.0f;
    float invincibility_frames_timer;


    [Header("Animations")]
    Animator animator;


    void Start() {
        animator = GetComponent<Animator>();
    }


    void Update() {
        invincibility_frames_timer = invincibility_frames_timer - Time.deltaTime;
        invincibility_frames_timer = Mathf.Max(invincibility_frames_timer, 0.0f);
    }


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibility_frames_timer <= 0) enemy_collision();
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibility_frames_timer <= 0) enemy_collision();
    }


    void enemy_collision() {
        GameManager.instance.player_takes_damage();
        invincibility_frames_timer = invincibility_frames_amount;

        if (GameManager.instance.player_is_dead) {
            disable_components_on_death();
            animator.SetTrigger("dead");
            Invoke("call_game_over_in_game_manager", animator.GetCurrentAnimatorStateInfo(0).length * 2);
        }
    }

    void disable_components_on_death() {
        GetComponent<Collider>().enabled = false;
        GetComponent<player_movement_controller>().enabled = false;
        GetComponent<rifle_controller>().enabled = false;
        
    }

    void call_game_over_in_game_manager() {
        GameManager.instance.game_over();
    }
}
