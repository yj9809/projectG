using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionManager : Singleton<OptionManager>
{
    // UI 요소들
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Toggle windowScreenToggle;
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private AudioMixer audioMixer;

    // 지원하는 해상도 리스트
    private List<Resolution> resolutions = new List<Resolution>()
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };

    // 게임 시작 시 호출
    void Start()
    {
        InitializeSettings();
        InitializeUI();
    }

    // 초기 설정을 로드하고 적용하는 메서드
    private void InitializeSettings()
    {
        audioMixer = AudioManager.Instance.audioMixer;

        // 볼륨 설정
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(masterVolumeSlider.value);
        SetBgmVolume(bgmVolumeSlider.value);
        SetSfxVolume(sfxVolumeSlider.value);

        // 초기 전체 화면 모드 및 윈도우 모드 설정
        bool isFullscreen = PlayerPrefs.GetInt("FullscreenPreference", 1) == 1;
        SetScreenMode(isFullscreen);

        // 해상도 설정
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionPreference", 0);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        ChangeResolution(savedResolutionIndex);
    }

    // UI 요소를 초기화하는 메서드
    private void InitializeUI()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        InitResolutionOptions();
        InitFullScreenToggle();
    }

    // 볼륨을 로그 스케일로 변환하여 설정하는 메서드
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

    // 전체 화면 모드와 윈도우 모드를 설정하는 메서드
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

    // 전체 화면 전환 설정 및 저장
    private void InitFullScreenToggle()
    {
        fullScreenToggle.onValueChanged.AddListener(isFullscreen => SetScreenMode(isFullscreen));
        windowScreenToggle.onValueChanged.AddListener(isWindowScreen => SetScreenMode(!isWindowScreen));
    }

    // 해상도 옵션 초기화 및 설정
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

    // 해상도 변경
    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
        PlayerPrefs.Save();
    }

    // 게임 종료
    public void OnExit()
    {
        Application.Quit();
    }

    // 옵션 값 재설정
    public void OptionValueSet()
    {
        InitializeSettings();
        InitializeUI();
    }
}
