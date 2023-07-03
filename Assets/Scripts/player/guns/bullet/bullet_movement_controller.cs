using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement_controller : MonoBehaviour
{
    public float travel_time = 3.0f;
    public float travel_speed = 10.0f;

    void Start() {
        Destroy(gameObject, travel_time);
    }

    void Update() {
        transform.position += transform.up * travel_speed * Time.deltaTime;
    }
}
