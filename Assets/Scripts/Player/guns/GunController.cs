using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    /// <summary>
    /// If it ain't broke, don't fix it.
    /// </summary>

    [Header("Stats")]
    public int damage = 1;
    public int maxMagazineSize = 30; 
    [SerializeField]private float timeBetweenShots = 0.1f; //Fire rate
    private float reloadSpeed;

    [Header("GunStates")]
    private int remainingBulletsInMagazine;
    private int amountOfBulletsShot;
    private bool shooting = false;
    private bool readyToShoot = true;
    private bool reloading = false;

    [Header("BulletInstantiation")]
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform bulletSpawnPoint;
    [SerializeField]private Transform bulletsGroup;

    [Header("ShootingAnimations")]
    private Animator animator;

    private AudioManagerComponent audioManagerComponent;

    private void Start() {   
        animator = GetComponent<Animator>();
        audioManagerComponent = GetComponent<AudioManagerComponent>();

        remainingBulletsInMagazine = maxMagazineSize;
    }

    private void Update() {
        KeyboardInput();
    }

    private void KeyboardInput() {
        shooting = Input.GetMouseButton(0);
        animator.SetBool("is_shooting", shooting);

        if (Input.GetKeyDown(KeyCode.R) && remainingBulletsInMagazine < maxMagazineSize && !reloading) 
            Reload();
        
        if (readyToShoot && shooting && !reloading && remainingBulletsInMagazine > 0) {
            if (GameManager.Instance.isPaused) return;
            Shoot();
        }
    }

    private void Shoot() {
        animator.SetBool("is_shooting", true);
        readyToShoot = false;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation, bulletsGroup);
        audioManagerComponent.Play("shooting");

        
        remainingBulletsInMagazine--;
        Invoke("ResetShot", timeBetweenShots);
    }

    private void ResetShot() {
        readyToShoot = true;
    }

    private void Reload() {
        animator.SetBool("is_shooting", false);
        animator.SetBool("is_empty", true);
        reloading = true;

        audioManagerComponent.Play("reload");

        animator.SetTrigger("reload");
        Invoke("ReloadFinished", animator.GetCurrentAnimatorStateInfo(1).length * 2);
    }

    private void ReloadFinished() {
        reloading = false;
        remainingBulletsInMagazine = maxMagazineSize;
        animator.SetBool("is_empty", false);
    }
}
