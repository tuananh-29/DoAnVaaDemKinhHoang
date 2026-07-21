using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    public Slider sliderMaster;
    public Slider sliderBGM;
    public Slider sliderSFX;

    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        sliderBGM.value = PlayerPrefs.GetFloat("BGMVol", 0.75f);
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);

        SetMasterVolume(sliderMaster.value);
        SetBGMVolume(sliderBGM.value);
        SetSFXVolume(sliderSFX.value);
    }

    public void SetMasterVolume(float value)
{
    mainMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20); 
    PlayerPrefs.SetFloat("MasterVol", value);
}

public void SetBGMVolume(float value)
{
    mainMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
    PlayerPrefs.SetFloat("BGMVol", value);
}

public void SetSFXVolume(float value)
{
    mainMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
    PlayerPrefs.SetFloat("SFXVol", value);
}

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}