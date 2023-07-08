using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{   
    public float maxHealth;
    [HideInInspector]public float currentHealth;

    private AudioManagerComponent audioManagerComponent;

    private void Start() {
        audioManagerComponent = GetComponent<AudioManagerComponent>();

        currentHealth = maxHealth;

    }

    public void DealDamage(float damageDealt) {
        currentHealth -= damageDealt;
        currentHealth = Mathf.Max(currentHealth, 0.0f);

        PlayAudio("hurt");
    }

    public void Heal(float healAmount) {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        PlayAudio("heal");
    }

    private void PlayAudio(string clipName) {
        if (audioManagerComponent == null) return;

        audioManagerComponent.Play(clipName);
    }
}
