using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitAura : MonoBehaviour
{
    [SerializeField]private float timeBetweenHeals;
    [SerializeField]private float healAmount;
    //[SerializeField]private float healRate;
    float healTimer;

    private void Update() {
        healTimer += Time.deltaTime;
        healTimer = Mathf.Min(healTimer, timeBetweenHeals + 1);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            RegenerateHealth();
        }
    }

    private void RegenerateHealth() {
        if (healTimer >= timeBetweenHeals) {
            healTimer = 0.0f;
            GameManager.Instance.HealPlayer(healAmount);
        }
    }
}
