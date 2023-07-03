﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpinner : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
