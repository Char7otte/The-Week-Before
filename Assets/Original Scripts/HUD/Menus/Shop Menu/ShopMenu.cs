using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject BGM;

    public bool shopMenuActiveBool = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (shopMenuActiveBool==true)
            {
                shopMenuActiveBool = false;

                shopMenu.SetActive(false);
                BGM.GetComponent<AudioSource>().UnPause();
            }
            else
            {
                shopMenuActiveBool = true;

                shopMenu.SetActive(true);
                BGM.GetComponent<AudioSource>().Pause();
            }
        }
    }
}
