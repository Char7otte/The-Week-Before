using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{   
    public float maxHealth;
    [HideInInspector]public float currentHealth = 1; //Has to be set above 0 because DeathComponent will think the player is dead before 
                                                     //before currentHealth is properly set otherwise. (Bug happens on restarts)
    private AudioManagerComponent audioManagerComponent;

    private void Start() {
        currentHealth = maxHealth;
        audioManagerComponent = GetComponent<AudioManagerComponent>();
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
