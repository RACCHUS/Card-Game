using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;

    private void Start()
    {
        // Get the available screen resolutions and populate the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        // Set the fullscreen toggle based on the current screen mode
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Change fullscreen mode
        Screen.fullScreen = isFullscreen;

        // Change resolution if fullscreen mode is enabled
        if (isFullscreen)
        {
            // Get the selected resolution from the dropdown
            int resolutionIndex = resolutionDropdown.value;
            Resolution resolution = resolutions[resolutionIndex];

            // Change the screen resolution
            Screen.SetResolution(resolution.width, resolution.height, true);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        // Change the screen resolution
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}