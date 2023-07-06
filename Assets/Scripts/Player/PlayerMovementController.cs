using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
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
    [SerializeField]private float staminaDrainSpeed = 2f;
    [SerializeField]private float staminaRegenerateSpeed = 1f;
    [SerializeField]private float timeBeforeStaminaRegenerates = 3;
    [SerializeField]float timer;
    [SerializeField]private float sprintSpeedMultiplier = 1.2f;
    

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        playerActionMoving = playerInput.actions["player_movement"];

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }


    private void Update() {
        movementVector = playerActionMoving.ReadValue<Vector3>();
        animator.SetBool("is_moving", movementVector.magnitude > 0);
    }


    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift) && GameManager.Instance.playerCurrentStamina > 0) 
        {
            animator.SetBool("is_sprinting", true);

            rb.AddForce(movementVector.normalized * movementSpeed * sprintSpeedMultiplier * Time.deltaTime);
            GameManager.Instance.playerCurrentStamina -= staminaDrainSpeed * Time.deltaTime;
            GameManager.Instance.playerCurrentStamina = Mathf.Max(GameManager.Instance.playerCurrentStamina, 0.0f);
            timer = 0.0f;
        }
        else
        {
            animator.SetBool("is_sprinting", false);

            rb.AddForce(movementVector.normalized * movementSpeed * Time.deltaTime);

            if (timer >= timeBeforeStaminaRegenerates) {
                GameManager.Instance.playerCurrentStamina += staminaRegenerateSpeed * Time.deltaTime;
                GameManager.Instance.playerCurrentStamina = Mathf.Min(GameManager.Instance.playerCurrentStamina, GameManager.Instance.playerMaxStamina);
            }
            else
                timer += Time.deltaTime;
        }
    }
}
