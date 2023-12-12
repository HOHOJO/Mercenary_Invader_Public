using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;

    private void Start()
    {
        InitializeSlider("MasterVol", masterSlider, masterLabel);
        InitializeSlider("MusicVol", musicSlider, musicLabel);
        InitializeSlider("SFXVol", sfxSlider, sfxLabel);
    }

    private void InitializeSlider(string parameterName, Slider slider, TMP_Text label)
    {
        float vol = 0f;
        audioMixer.GetFloat(parameterName, out vol);
        slider.value = vol;
        SetVolume(parameterName, slider, label);
    }

    private void SetVolume(string parameterName, Slider slider, TMP_Text label)
    {
        label.text = Mathf.RoundToInt(slider.value + 80).ToString();
        audioMixer.SetFloat(parameterName, slider.value);
    }

    public void SetMasterVol()
    {
        SetVolume("MasterVol", masterSlider, masterLabel);
    }

    public void SetMusicVol()
    {
        SetVolume("MusicVol", musicSlider, musicLabel);
    }

    public void SetSFXVol()
    {
        SetVolume("SFXVol", sfxSlider, sfxLabel);
    }
}
