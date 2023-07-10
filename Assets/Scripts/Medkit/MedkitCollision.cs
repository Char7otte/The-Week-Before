using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitCollision : MonoBehaviour
{
    [SerializeField]private float rotateSpeed;

    [SerializeField]private float medkitLifetime;
    [SerializeField]private GameObject medkitAura;

    private void Awake() {
        Destroy(gameObject, medkitLifetime);
    }

    private void Update() {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            medkitAura.SetActive(true);
        }
    }
}
