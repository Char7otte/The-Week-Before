using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseBehavior : MonoBehaviour
{
    //---Chasing the player---
    GameObject playerCharacter;
    public float movementSpeed = 4f;

    void Start()
    {
        playerCharacter = GameObject.Find("Player Character");
    }

    void Update()
    {
        //Chasing the player
        transform.LookAt(playerCharacter.transform.position);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}

