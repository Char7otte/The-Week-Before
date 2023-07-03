using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeTextUI = null;

    void Start()
    {
        LoadValues();
    }
    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = "Volume: " + (volume * 100).ToString("0") + "%";
    }

    public void SaveVolumeButton()
    {
        float volumeValue= volumeSlider.value;
        PlayerPrefs.SetFloat("GameValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("GameValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
