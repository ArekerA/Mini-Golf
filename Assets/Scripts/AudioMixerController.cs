using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public SaveFileManager saveFileManager;
    public Slider soundtrackVolumeSlider;
    public Slider effectVolumeSlider;
    public void Start()
    {
        saveFileManager.Load();
        soundtrackVolumeSlider.value = StaticAudio.soundtrackVolume;
        Debug.Log(soundtrackVolumeSlider.value);
        effectVolumeSlider.value = StaticAudio.effectVolume;
    }
    public void SetSoundtrackVolume(float a)
    {
        audioMixer.SetFloat("SoundtrackVolume", a);
        StaticAudio.soundtrackVolume = a;
        saveFileManager.Save();
    }
    public void SetEffectkVolume(float a)
    {
        audioMixer.SetFloat("EffectVolume", a);
        StaticAudio.effectVolume = a;
        saveFileManager.Save();
    }
}
