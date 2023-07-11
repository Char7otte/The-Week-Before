using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance;

    public static string playerCharacterSelected;


    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void SelectCharacter(string character) {
        playerCharacterSelected = character;
    }
}
