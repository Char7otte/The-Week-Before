using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementController : MonoBehaviour
{
    public float travelTime = 3.0f;
    public float movementSpeed = 10.0f;

    private void Start() {
        Destroy(gameObject, travelTime);
    }

    private void Update() {
        transform.position += transform.up * movementSpeed * Time.deltaTime;
    }
}
