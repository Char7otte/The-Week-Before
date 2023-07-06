using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    [Header("GunStats")]
    public int damage = 2;
    [SerializeField]private int maxMagazineSize = 30;
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

    [Header("HUD")]
    [SerializeField]private TMP_Text ammoCountText;

    [Header("SFX")]
    [SerializeField]private AudioClip[] audioClip;
    private AudioSource audioSource;

    private void Start() {
        remainingBulletsInMagazine = maxMagazineSize;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }


    private void Update() {
        keyboard_input();
        //play_animations();
        ammoCountText.SetText(remainingBulletsInMagazine + " / " + maxMagazineSize);
    }


    private void keyboard_input() {
        shooting = Input.GetMouseButton(0);
        animator.SetBool("is_shooting", shooting);

        if (Input.GetKeyDown(KeyCode.R) && remainingBulletsInMagazine < maxMagazineSize && !reloading) 
            reload();
        
        if (readyToShoot && shooting && !reloading && remainingBulletsInMagazine > 0) {
            shoot();
        }
    }

    private void shoot() {
        animator.SetBool("is_shooting", true);
        readyToShoot = false;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation, bulletsGroup);
        audioSource.PlayOneShot(audioClip[0]);
        
        remainingBulletsInMagazine--;
        Invoke("resetShot", timeBetweenShots);
    }

    private void resetShot() {
        readyToShoot = true;
    }

    private void reload() {
        animator.SetBool("is_shooting", false);
        animator.SetBool("is_empty", true);
        reloading = true;

        audioSource.PlayOneShot(audioClip[1]);

        animator.SetTrigger("reload");
        Invoke("reloadFinished", animator.GetCurrentAnimatorStateInfo(1).length * 2);
    }

    private void reloadFinished() {
        reloading = false;
        remainingBulletsInMagazine = maxMagazineSize;
        animator.SetBool("is_empty", false);
    }

    // void play_animations() {
    //     var is_shooting = shooting;
    //     var is_empty = remainingBulletsInMagazine < 1;

    //     animator.SetBool("is_shooting", is_shooting);
    //     animator.SetBool("is_empty", is_empty);
    // }
}
