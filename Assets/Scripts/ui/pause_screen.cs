using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_screen : MonoBehaviour
{
    void OnEnable() {
        Time.timeScale = 0.0f;
    }

    void OnDisable() {
        Time.timeScale = 1.0f;
    }
}
