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
        if (collision.gameObject.tag == "Enemy" && invincibilityFramesTimer <= 0) EnemyCollision();
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibilityFramesTimer <= 0) EnemyCollision();
    }

    void EnemyCollision() {
        GameManager.Instance.PlayerTakesDamage();
        invincibilityFramesTimer = invincibilityFramesDuration;

        if (GameManager.Instance.isPlayerDead) {
            DisableComponentsUponDeath();
            animator.SetTrigger("dead");
            Invoke("CallGameOverInGameManager", animator.GetCurrentAnimatorStateInfo(0).length * 2);
        }
    }

    void DisableComponentsUponDeath() {
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<GunController>().enabled = false;
        
    }

    void CallGameOverInGameManager() {
        GameManager.Instance.GameOver();
    }
}
