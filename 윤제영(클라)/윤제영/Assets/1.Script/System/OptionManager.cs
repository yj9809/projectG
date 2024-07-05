using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionManager : Singleton<OptionManager>
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    void Start()
    {
        LoadSettings();
        InitResolutionOptions();

        fullScreenToggle.onValueChanged.AddListener(SetFullScreenMode);
    }

    void OnApplicationQuit()
    {
        SaveSettings();
    }
    void SetFullScreenMode(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }
    private void InitResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        // ���͸��� �ػ� �� �ֻ���
        int[] desiredWidths = { 1280, 1600, 1920 };
        int[] desiredHeights = { 720, 900, 1080 };

        foreach (Resolution resolution in Screen.resolutions)
        {
            // ���͸� ���� Ȯ��
            if ((resolution.width == desiredWidths[0] && resolution.height == desiredHeights[0] ||
                 resolution.width == desiredWidths[1] && resolution.height == desiredHeights[1] ||
                 resolution.width == desiredWidths[2] && resolution.height == desiredHeights[2]))
            {
                options.Add(resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz");
            }
        }

        resolutionDropdown.AddOptions(options);

        // �⺻�� ����
        if (options.Count > 0)
        {
            resolutionDropdown.value = 0; // ù ��° ���͵� �ػ󵵸� �⺻������ ����
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 0) == 1;

        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
    }

    public void OnResolutionChange(int index)
    {
        string[] parts = resolutionDropdown.options[index].text.Split(new char[] { 'x', ' ' });
        int width = int.Parse(parts[0]);
        int height = int.Parse(parts[1]);
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreen", fullScreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 0) == 1;

        Resolution selectedResolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);
        fullScreenToggle.isOn = isFullScreen;
    }
}

