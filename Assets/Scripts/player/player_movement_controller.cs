using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement_controller : MonoBehaviour
{
    public GameManager gm;

    [Header("Inputs")]
    PlayerInput player_input;
    InputAction player_action_moving;

    [Header("PlayerMovement")]
    public float movement_velocity = 1500.0f;
    Rigidbody rb;
    Vector3 movement_vector;

    [Header("MovementAnimations")]
    Animator animator;

    [Header("StaminaSystem")]
    public float stamina_drain_speed = 2f;
    public float stamina_regenerate_speed = 1f;
    public float time_before_stamina_regenerates = 3;
    [SerializeField]float timer;
    public float sprint_speed_multiplier = 1.2f;
    

    void Start() {
        player_input = GetComponent<PlayerInput>();
        player_action_moving = player_input.actions["player_movement"];

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }


    void Update() {
        movement_vector = player_action_moving.ReadValue<Vector3>();
        animator.SetBool("is_moving", movement_vector.magnitude > 0);
    }


    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift) && GameManager.instance.player_current_stamina > 0) 
        {
            animator.SetBool("is_sprinting", true);

            rb.AddForce(movement_vector.normalized * movement_velocity * sprint_speed_multiplier * Time.deltaTime);
            GameManager.instance.player_current_stamina -= stamina_drain_speed * Time.deltaTime;
            GameManager.instance.player_current_stamina = Mathf.Max(GameManager.instance.player_current_stamina, 0.0f);
            timer = 0.0f;
        }
        else
        {
            animator.SetBool("is_sprinting", false);

            rb.AddForce(movement_vector.normalized * movement_velocity * Time.deltaTime);

            if (timer >= time_before_stamina_regenerates) {
                GameManager.instance.player_current_stamina += stamina_regenerate_speed * Time.deltaTime;
                GameManager.instance.player_current_stamina = Mathf.Min(GameManager.instance.player_current_stamina, GameManager.instance.player_max_stamina);
            }
            else
                timer += Time.deltaTime;
        }
    }
}
