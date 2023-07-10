using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitAura : MonoBehaviour
{
    [SerializeField]private float timeBetweenHeals;
    [SerializeField]private float healAmount;
    //[SerializeField]private float healRate;

    private float healTimer;

    private void Update() {
        healTimer += Time.deltaTime;
        healTimer = Mathf.Min(healTimer, timeBetweenHeals + 1);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (healTimer >= timeBetweenHeals) {
                healTimer = 0.0f;
                other.gameObject.GetComponent<HealthComponent>().Heal(healAmount);
            }
        }
    }
}
