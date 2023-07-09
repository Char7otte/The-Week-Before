using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaComponent : MonoBehaviour
{
    public float maxStamina = 100;
    [HideInInspector]public float currentStamina;
    public float staminaDrainSpeed = 30f;
    public float staminaRegenerateSpeed = 5f;
    public float timeBeforeStaminaRegenerates = 3;
    public float sprintSpeedMultiplier = 1.2f;

    private void Start() {
        currentStamina = maxStamina;
    }
}
