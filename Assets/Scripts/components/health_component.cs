using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_component : MonoBehaviour
{   
    public float max_health;

    [HideInInspector]public float health;

    void Start() {
        health = max_health;
    }

    public void deal_damage(int damage_dealt) {
        health -= damage_dealt;
        health = Mathf.Max(health, 0.0f);
    }

    public bool is_dead_signal() {
        if (health <= 0) return true;
        return false;
    }
}
