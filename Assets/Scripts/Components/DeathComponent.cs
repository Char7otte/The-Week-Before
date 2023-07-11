using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    [HideInInspector]public bool isAlive = true;

    [SerializeField]private Behaviour[] componentsToDisable;

    private HealthComponent healthComponent;
    private AudioManagerComponent audioManagerComponent;
    private Animator animator;

    private void Start() {
        healthComponent = GetComponent<HealthComponent>();
        audioManagerComponent = GetComponent<AudioManagerComponent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (healthComponent.currentHealth <= 0 && isAlive == true) {
            Died();
        }
    }

    private void Died() {
        isAlive = false;

        GameManager.Instance.killCount++;
        
        animator.SetTrigger("dead");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length * 2);

        PlayAudio("died");
        DisableTheseComponentsUponDeath();
    }

    private void DisableTheseComponentsUponDeath() {
        GetComponent<Collider>().enabled = false; //Stuff like this is why I prefer Godot. Colliders don't inherit from Behaviour for some reason.

        foreach(Behaviour behaviour in componentsToDisable) {
            behaviour.enabled = false;
        }
    }

    private void PlayAudio(string clipName) {
        if (audioManagerComponent == null) return;
        
        audioManagerComponent.Play(clipName);
    }
}
