using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]private AudioMixerGroup musicVolumeSlider;
    [SerializeField]private AudioMixerGroup sfxVolumeSlider;
    [SerializeField]private Sound[] sounds;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        foreach (Sound s in sounds) {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.isLooping;
            s.audioSource.volume = s.volume;

            switch (s.audioType) {
                case Sound.AudioType.music:
                    s.audioSource.outputAudioMixerGroup = musicVolumeSlider;
                    break;
                case Sound.AudioType.sfx:
                    s.audioSource.outputAudioMixerGroup = sfxVolumeSlider;
                    break;
                default:
                    break;             
            }

            if (s.playOnAwake) s.audioSource.Play();
        }
    }

    public void Play(string clipname) {
        Sound s = Array.Find(sounds, dummy_sound => dummy_sound.clipName == clipname);
        if  (s == null) {
            print(clipname + " does not exist.");
            return;
        }
        
        s.audioSource.Play();
    }

    public void Stop(string clipname) {
        Sound s = Array.Find(sounds, dummy_sound => dummy_sound.clipName == clipname);
        if  (s == null) {
            print(clipname + " does not exist.");
            return;
        }
        
        s.audioSource.Stop();
    }

}
