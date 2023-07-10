using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            AudioManagerMaster.Instance.Play("PointPickup");
            SaveDataManager.pointsCollected++;
            Destroy(gameObject);
        }       
    }
}
