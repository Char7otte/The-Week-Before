using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollision : MonoBehaviour
{
    [SerializeField]private float rotationSpeed;

    private void Update() {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            AudioManagerMaster.Instance.Play("PointPickup");
            SaveDataManager.pointsCollected++;
            Destroy(gameObject);
        }       
    }
}
