using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeElapsed : MonoBehaviour
{
    public static float time;
    public static float minutes;
    public static float seconds;

    Text timeText;
    public Text timePassedTextInGameOver;

    void Start()
    {
        timeText=GetComponent<Text>();
        KillCounter.killCounter = 0;
    }

    void Update()
    { 
        time = Time.timeSinceLevelLoad;

        minutes = Mathf.FloorToInt(time/60f);
        seconds = Mathf.FloorToInt(time - minutes * 60);

        timeText.text = "" + minutes + " : " + seconds;
        timePassedTextInGameOver.text = timeText.text;
    }
}
