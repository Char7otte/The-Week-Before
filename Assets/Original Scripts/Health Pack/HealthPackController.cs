using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    public GameObject healingField;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healingField.SetActive(true);
        }
    }
}
