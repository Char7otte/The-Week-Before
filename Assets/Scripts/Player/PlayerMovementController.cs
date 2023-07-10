using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    /// <summary>
    /// If it ain't broke, don't fix it.
    /// </summary>

    [Header("Inputs")]
    private PlayerInput playerInput;
    private InputAction playerActionMoving;

    [Header("PlayerMovement")]
    [SerializeField]private float movementSpeed = 1500.0f;
    private Rigidbody rb;
    private Vector3 movementVector;

    [Header("MovementAnimations")]
    private Animator animator;

    [Header("StaminaSystem")]
    [HideInInspector]public float maxStamina;
    [HideInInspector]public float currentStamina;
    private float staminaDrainSpeed;
    private float staminaRegenerateSpeed;
    private float timeBeforeStaminaRegenerates;
    private float sprintSpeedMultiplier;
    private float timer;
    

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        playerActionMoving = playerInput.actions["player_movement"];

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        var staminaComponent = GetComponent<StaminaComponent>();
        maxStamina = staminaComponent.maxStamina;
        currentStamina = staminaComponent.currentStamina;
        staminaDrainSpeed = staminaComponent.staminaDrainSpeed;
        staminaRegenerateSpeed = staminaComponent.staminaRegenerateSpeed;
        timeBeforeStaminaRegenerates = staminaComponent.timeBeforeStaminaRegenerates;
        sprintSpeedMultiplier = staminaComponent.sprintSpeedMultiplier;

        movementSpeed = movementSpeed + (movementSpeed * 0.1f * SaveDataManager.speedUpgradeCount);
    }

    private void Update() {
        movementVector = playerActionMoving.ReadValue<Vector3>();
        animator.SetBool("is_moving", movementVector.magnitude > 0);
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0) 
        {
            animator.SetBool("is_sprinting", true);

            rb.AddForce(movementVector.normalized * movementSpeed * sprintSpeedMultiplier * Time.deltaTime);
            currentStamina -= staminaDrainSpeed * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0.0f);
            timer = 0.0f;
        }
        else
        {
            animator.SetBool("is_sprinting", false);

            rb.AddForce(movementVector.normalized * movementSpeed * Time.deltaTime);

            if (timer >= timeBeforeStaminaRegenerates) {
                currentStamina += staminaRegenerateSpeed * Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
            else
                timer += Time.deltaTime;
        }
    }
}
