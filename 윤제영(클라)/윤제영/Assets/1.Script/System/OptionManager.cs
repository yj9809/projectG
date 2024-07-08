using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionManager : Singleton<OptionManager>
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private AudioMixer audioMixer;

    private List<Resolution> resolutions = new List<Resolution>()
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };
    void Start()
    {
        audioMixer = AudioManager.Instance.audioMixer;

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(masterVolumeSlider.value);
        SetBgmVolume(bgmVolumeSlider.value);
        SetSfxVolume(sfxVolumeSlider.value);

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        InitResolutionOptions();
        InitFullscreenToggle();

    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("Bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    private void InitFullscreenToggle()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(SetFullscreen);
    }
    private void InitResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var res in resolutions)
        {
            options.Add(res.width + "x" + res.height);
        }
        resolutionDropdown.AddOptions(options);

        // Load saved resolution index
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", 0);
        resolutionDropdown.RefreshShownValue();
        ChangeResolution(resolutionDropdown.value);

        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("FullscreenPreference", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
        PlayerPrefs.Save();
    }
    public void OnExit()
    {
        Application.Quit();
    }
    public void OptionValueSet()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMasterVolume(masterVolumeSlider.value);
        SetBgmVolume(bgmVolumeSlider.value);
        SetSfxVolume(sfxVolumeSlider.value);

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        InitResolutionOptions();
        InitFullscreenToggle();
    }
}

