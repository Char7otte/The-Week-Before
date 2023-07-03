using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopMenuButtons : MonoBehaviour
{
    public PlayerController playerCharacterScript;
    public GameObject BGM;

    public GameObject shopMenu;

    public AudioClip[] audioClip;
    AudioSource audioSource;

    public ShopMenu script;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PurchaseAmmo()
    {
        if (playerCharacterScript.points >= playerCharacterScript.ammoPurchaseCost)
        {
            playerCharacterScript.points-=playerCharacterScript.ammoPurchaseCost;
            playerCharacterScript.ammo += playerCharacterScript.ammoPurchaseGain;
            audioSource.PlayOneShot(audioClip[0]);
        }
        else
        {
            audioSource.PlayOneShot(audioClip[1]);
        }
    }

    public void GoBackToGame()
    {
        //script.shopMenuActiveBool = false;
        shopMenu.SetActive(false);
        BGM.GetComponent<AudioSource>().UnPause();
    }
}
