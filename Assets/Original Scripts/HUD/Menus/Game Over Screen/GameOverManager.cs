using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject BGM;
    void Start()
    {
        Time.timeScale = 0.0f;
        BGM.GetComponent<AudioSource>().Pause();
    }
}
