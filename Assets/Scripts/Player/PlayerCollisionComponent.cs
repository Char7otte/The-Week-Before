using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionComponent : MonoBehaviour 
{  
    [Header("InvincibilityFrames")]
    [SerializeField]private float invincibilityFramesDuration = 1.0f;
    private float invincibilityFramesTimer;

    private void Update() {
        invincibilityFramesTimer = invincibilityFramesTimer - Time.deltaTime;
        invincibilityFramesTimer = Mathf.Max(invincibilityFramesTimer, 0.0f);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && invincibilityFramesTimer <= 0) {
            var enemyDamage = collision.gameObject.GetComponent<DamageComponent>().damage;
            EnemyCollision(enemyDamage);
        }
    }

    private void EnemyCollision(float enemyDamage) {
        invincibilityFramesTimer = invincibilityFramesDuration;

        var enemyDamageAfterScaling = enemyDamage + (GameManager.Instance.difficultyScaleMultiplier * GameManager.Instance.difficultyScaleMultiplierAmount);
        var healthComponent = GetComponent<HealthComponent>();
        healthComponent.DealDamage(enemyDamageAfterScaling);
    }
}
