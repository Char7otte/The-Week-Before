using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsMenuButtons : MonoBehaviour
{
    public GameObject settingsMenu;


    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
