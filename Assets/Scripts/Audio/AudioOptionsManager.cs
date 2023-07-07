using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public static float masterVolume {get; private set;}
    public static float musicVolume { get; private set; }
    public static float SFXVolume {get; private set; }

    [SerializeField]private TextMeshProUGUI masterSliderPercentageText;
    [SerializeField]private Slider masterSlider;
    [SerializeField]private TextMeshProUGUI musicSliderPercentageText;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private TextMeshProUGUI SFXSliderPercentageText;
    [SerializeField]private Slider SFXSlider;

    public void OnMasterSliderValueChange() {
        masterVolume = masterSlider.value;
        masterSliderPercentageText.text = ((int)(masterVolume*100)).ToString() +  "%";
        AudioManager.Instance.UpdateAudioMixer();
    }

    public void OnMusicSliderValueChange() {
        musicVolume = musicSlider.value;
        musicSliderPercentageText.text = ((int)(musicVolume*100)).ToString() + "%";
        AudioManager.Instance.UpdateAudioMixer();
    }

    public void OnSFXSliderValueChange() {
        SFXVolume = SFXSlider.value;
        SFXSliderPercentageText.text = ((int)(SFXVolume*100)).ToString() + "%";
        AudioManager.Instance.UpdateAudioMixer();
    }
}
