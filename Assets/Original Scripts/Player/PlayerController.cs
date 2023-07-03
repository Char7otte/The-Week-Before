using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //---Movement---
    Rigidbody rigidBody;
    public float movementSpeed = 1.5f, movementSpeedMultiplier = 2f;
    Vector3 movement;
    //-Sprinting & Stamina
    public float maxStamina = 100f;
    float stamina;
    float staminaSinceLastActivation;
    public float staminaDecreasePerFrame = 20, staminaIncreasePerFrame = 10;
    public float staminaTimeBeforeRegen = 2;

    //---Shooting---
    public GameObject bulletSpawnPoint, bulletPrefab;
    public float fireRate = 0.2f;
    float nextFire;
    public int ammo = 200;

    //---Collision---
    //-Health & Enemy Damage
    public float maxHealth = 100f;
    [HideInInspector] public float health;
    public float enemyDamage = 5;
    //-Points
    [HideInInspector] public int points;
    public int pointsValueOnPickup = 10;
    //-Healing Field Thing
    public float healAmount = 3f;
    public float timeToHeal = 2f;
    float sinceLastHeal;

    //---Animations---
    public GameObject playerCharacterModel;
    Animator animator;

    //---Sounds---
    AudioSource audioSource;
    public AudioClip[] audioClip;
    /*List of sounds:
    Gun shots
    Death Sound
    Coin Pickup Sound
    Heal sound
     */

    //---UI---
    public Text ammoText, healthText, staminaText, pointsText;
    public Image healthBar, staminaBar;
    //-Shop-
    public int ammoPurchaseCost = 50;
    public int ammoPurchaseGain = 35;
    public Text ammoCostText;
    //-Game Over
    public GameObject gameOverMenu;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = playerCharacterModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stamina = maxStamina;
        health = maxHealth;
    }

    void Update()
    {
        LookingAroundWithMouse();

        if (Input.GetMouseButton(0) && ammo > 0 && Time.time > nextFire)
            Shooting();

        UpdatingUIElements();

        //Movement Animations & Walking Sounds
        if (rigidBody.velocity.magnitude > 1)
        {
            animator.SetBool("Walking", true);
        }
        else
            animator.SetBool("Walking", false);

        //Prevent player from going too far
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -29, 29), 1, (Mathf.Clamp(transform.position.z, -29, 29)));

        //Check if the player has enough points to buy something from the shop
        if (points < ammoPurchaseCost)
            ammoCostText.color = Color.red;
        else
            ammoCostText.color = Color.green;
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) 
        {
                rigidBody.AddForce(movement * movementSpeed * movementSpeedMultiplier / Time.deltaTime);
                stamina = Mathf.Clamp(stamina - (staminaDecreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
                staminaSinceLastActivation = 0.0f;
        }
        else
        {
            rigidBody.AddForce(movement * movementSpeed / Time.deltaTime);

            if (staminaSinceLastActivation >= staminaTimeBeforeRegen)
                stamina = Mathf.Clamp(stamina + (staminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
            else
                staminaSinceLastActivation += Time.deltaTime;
        }
    }

    void LookingAroundWithMouse()
    {
        var mouseLookPos = new Vector3(0, 0, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            mouseLookPos = hit.point;

        var lookDir = mouseLookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void Shooting()
    {
        nextFire = Time.time + fireRate;
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
        ammo -= 1;

        //Gun Firing Animation
        animator.SetTrigger("Shooting");

        //Gun Firing Sound
        audioSource.PlayOneShot(audioClip[0]);
    }

    void UpdatingUIElements()
    {
        //Ammo
        ammoText.text = ammo.ToString();

        //Stamina
        staminaBar.fillAmount = stamina / maxStamina;
        staminaText.text = "Stamina: " + Mathf.Round(stamina);

        //Health
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "Health: " + Mathf.Round(health);

        //Points
        pointsText.text = points.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= enemyDamage;

            if (health <= 0)
            {
                GetComponent<BoxCollider>().enabled = false;
                movementSpeed = 0;

                //Death Animation
                animator.SetTrigger("Death");

                //Death Sound
                audioSource.PlayOneShot(audioClip[1]);

                gameOverMenu.SetActive(true);
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            points += pointsValueOnPickup;

            //Coin Pickup sound
            audioSource.PlayOneShot(audioClip[2]);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("HealingCircle"))
        {
            sinceLastHeal += Time.deltaTime;

            if (sinceLastHeal > timeToHeal)
            {
                health += healAmount;
                sinceLastHeal = 0f;
                audioSource.PlayOneShot(audioClip[3]);

                //Preventing Health from going over the max
                health = Mathf.Clamp(health, 0.0f, maxHealth);
            }
        }
    }
}
