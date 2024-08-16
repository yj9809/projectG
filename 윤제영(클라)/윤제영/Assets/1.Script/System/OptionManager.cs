using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionManager : Singleton<OptionManager>
{
    // UI ��ҵ�
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Toggle windowScreenToggle;
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private AudioMixer audioMixer;

    // �����ϴ� �ػ� ����Ʈ
    private List<Resolution> resolutions = new List<Resolution>()
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };

    // ���� ���� �� ȣ��
    void Start()
    {
        InitializeSettings();
        InitializeUI();
    }

    // �ʱ� ������ �ε��ϰ� �����ϴ� �޼���
    private void InitializeSettings()
    {
        audioMixer = AudioManager.Instance.audioMixer;

        // ���� ����
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(masterVolumeSlider.value);
        SetBgmVolume(bgmVolumeSlider.value);
        SetSfxVolume(sfxVolumeSlider.value);

        // �ʱ� ��ü ȭ�� ��� �� ������ ��� ����
        bool isFullscreen = PlayerPrefs.GetInt("FullscreenPreference", 1) == 1;
        SetScreenMode(isFullscreen);

        // �ػ� ����
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionPreference", 0);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        ChangeResolution(savedResolutionIndex);
    }

    // UI ��Ҹ� �ʱ�ȭ�ϴ� �޼���
    private void InitializeUI()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        InitResolutionOptions();
        InitFullScreenToggle();
    }

    // ������ �α� �����Ϸ� ��ȯ�Ͽ� �����ϴ� �޼���
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

    // ��ü ȭ�� ���� ������ ��带 �����ϴ� �޼���
    public void SetScreenMode(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.fullScreen = true;
            fullScreenToggle.isOn = true;
            windowScreenToggle.isOn = false;
        }
        else
        {
            Screen.fullScreen = false;
            fullScreenToggle.isOn = false;
            windowScreenToggle.isOn = true;
        }

        PlayerPrefs.SetInt("FullscreenPreference", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ��ü ȭ�� ��ȯ ���� �� ����
    private void InitFullScreenToggle()
    {
        fullScreenToggle.onValueChanged.AddListener(isFullscreen => SetScreenMode(isFullscreen));
        windowScreenToggle.onValueChanged.AddListener(isWindowScreen => SetScreenMode(!isWindowScreen));
    }

    // �ػ� �ɼ� �ʱ�ȭ �� ����
    private void InitResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var res in resolutions)
        {
            options.Add(res.width + "x" + res.height);
        }
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    // �ػ� ����
    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
        PlayerPrefs.Save();
    }

    // ���� ����
    public void OnExit()
    {
        Application.Quit();
    }

    // �ɼ� �� �缳��
    public void OptionValueSet()
    {
        InitializeSettings();
        InitializeUI();
    }
}
