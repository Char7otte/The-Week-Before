using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisions : MonoBehaviour
{  
    [Header("InvincibilityFrames")]
    [SerializeField]private float invincibilityFramesDuration = 1.0f;
    private float invincibilityFramesTimer;

    [Header("Animations")]
    private Animator animator;


    void Start() {
        animator = GetComponent<Animator>();
    }


    void Update() {
        invincibilityFramesTimer = invincibilityFramesTimer - Time.deltaTime;
        invincibilityFramesTimer = Mathf.Max(invincibilityFramesTimer, 0.0f);
    }


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibilityFramesTimer <= 0) enemyCollision();
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibilityFramesTimer <= 0) enemyCollision();
    }


    void enemyCollision() {
        GameManager.Instance.playerTakesDamage();
        invincibilityFramesTimer = invincibilityFramesDuration;

        if (GameManager.Instance.isPlayerDead) {
            disableComponentsUponDeath();
            animator.SetTrigger("dead");
            Invoke("callGameOverInGameManager", animator.GetCurrentAnimatorStateInfo(0).length * 2);
        }
    }

    void disableComponentsUponDeath() {
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<GunController>().enabled = false;
        
    }

    void callGameOverInGameManager() {
        GameManager.Instance.gameOver();
    }
}
