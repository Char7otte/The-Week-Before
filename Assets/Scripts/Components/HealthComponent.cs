using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{   
    public float maxHealth;

    [HideInInspector]public float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void dealDamage(int damageDealt) {
        currentHealth -= damageDealt;
        currentHealth = Mathf.Max(currentHealth, 0.0f);
    }

    public bool isDeadSignal() {
        if (currentHealth <= 0) return true;
        return false;
    }
}
