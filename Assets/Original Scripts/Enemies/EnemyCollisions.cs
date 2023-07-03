using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    public float health = 5f;
    public float bulletDamage = 1f;

    public GameObject coinPrefab;

    //---Health Pack---
    public GameObject healthPackPrefab;
    public int RNG = 20;

    //---Animations---
    public GameObject zombieModel;
    Animator animator;

    //---Audio---
    public AudioClip[] audioClip;
    AudioSource audioSource;

    void Start()
    {
        animator = zombieModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitForAttackAnimationToFinish());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            health -= bulletDamage;

            if (health<=0)
            {
                StartCoroutine(Despawn());
            }
        }
    }

    IEnumerator WaitForAttackAnimationToFinish()
    {
        //Stops the enemy from moving until the attack animation is finished.
        GetComponent<EnemyChaseBehavior>().enabled = false;
        animator.SetTrigger("Attack");

        while (!animator.IsInTransition(0))
        {
            yield return null;
        }

        GetComponent<EnemyChaseBehavior>().enabled = true;
    }

    IEnumerator Despawn()
    {
        //This works?????
        audioSource.PlayOneShot(audioClip[0]);
        animator.SetTrigger("Death");
        GetComponent<EnemyChaseBehavior>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        SpawnCoin();
        SpawnHealthPack();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        KillCounter.killCounter++;
    }

    void SpawnCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.Euler(90,0,0));
    }

    void SpawnHealthPack()
    {
        var randomNumber = Random.Range(1, RNG);

        if (randomNumber == 1)
        {
            Instantiate(healthPackPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        }

    }
}
