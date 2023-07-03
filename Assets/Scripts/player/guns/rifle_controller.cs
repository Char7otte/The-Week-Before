using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rifle_controller : MonoBehaviour
{
    [Header("GunStats")]
    public int damage = 2;
    public int max_magazine_size = 30;
    public float time_between_shots = 0.1f; //Fire rate
    public float reload_speed;

    [Header("GunStates")]
    int remaining_bullets_in_magazine;
    int amount_of_bullets_shot;
    bool shooting = false;
    bool ready_to_shoot = true;
    bool reloading = false;

    [Header("BulletInstantiation")]
    public GameObject bullet_prefab;
    public Transform bullet_spawn_point;
    public Transform parent_of_bullets;

    [Header("ShootingAnimations")]
    Animator animator;

    [Header("HUD")]
    public TMP_Text ammo_count_text;

    [Header("SFX")]
    public AudioClip[] audio_clip;
    AudioSource audio_source;

    void Start() {
        remaining_bullets_in_magazine = max_magazine_size;
        animator = GetComponent<Animator>();

        audio_source = GetComponent<AudioSource>();
    }


    void Update() {
        keyboard_input();
        //play_animations();
        ammo_count_text.SetText(remaining_bullets_in_magazine + " / " + max_magazine_size);
    }


    void keyboard_input() {
        shooting = Input.GetMouseButton(0);
        animator.SetBool("is_shooting", shooting);

        if (Input.GetKeyDown(KeyCode.R) && remaining_bullets_in_magazine < max_magazine_size && !reloading) 
            reload();
        
        if (ready_to_shoot && shooting && !reloading && remaining_bullets_in_magazine > 0) {
            shoot();
        }
    }

    void shoot() {
        animator.SetBool("is_shooting", true);
        ready_to_shoot = false;

        Instantiate(bullet_prefab, bullet_spawn_point.position, transform.rotation, parent_of_bullets);
        audio_source.PlayOneShot(audio_clip[0]);
        
        remaining_bullets_in_magazine--;
        Invoke("reset_shot", time_between_shots);
    }

    void reset_shot() {
        ready_to_shoot = true;
    }

    void reload() {
        animator.SetBool("is_shooting", false);
        animator.SetBool("is_empty", true);
        reloading = true;

        audio_source.PlayOneShot(audio_clip[1]);

        animator.SetTrigger("reload");
        Invoke("reload_finished", animator.GetCurrentAnimatorStateInfo(1).length * 2);
    }

    void reload_finished() {
        reloading = false;
        remaining_bullets_in_magazine = max_magazine_size;
        animator.SetBool("is_empty", false);
    }

    void play_animations() {
        var is_shooting = shooting;
        var is_empty = remaining_bullets_in_magazine < 1;

        animator.SetBool("is_shooting", is_shooting);
        animator.SetBool("is_empty", is_empty);
    }
}
