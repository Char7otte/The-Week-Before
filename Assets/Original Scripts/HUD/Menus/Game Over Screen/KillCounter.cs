using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public static int killCounter;

    void Start()
    {
        GetComponent<Text>().text = "Kills: " + killCounter;
    }


    
}
