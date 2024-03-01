using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value).ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        GetResolution(PlayerPrefs.GetInt("resolution"));
        GetVolume(PlayerPrefs.GetFloat("musicVolume"));
        GetQuality(PlayerPrefs.GetInt("quality"));
        if (PlayerPrefs.GetString("fullscreen").ToLower() == "false")
            GetFullscreen(false);
        else
            GetFullscreen(true);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        transform.Find("VolumeSlider").GetComponent<Slider>().value = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetString("fullscreen", isFullscreen.ToString());
    }

    public void GetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        transform.Find("ResolutionDropdown").GetComponentInChildren<TMP_Dropdown>().value = resolutionIndex;
    }

    public void GetVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        transform.Find("VolumeSlider").GetComponent<Slider>().value = volume;
    }

    public void GetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        transform.Find("GraphicsDropdown").GetComponentInChildren<TMP_Dropdown>().value = qualityIndex;
    }

    public void GetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        transform.Find("FullscreenToggle").GetComponentInChildren<Toggle>().isOn = isFullscreen;
    }
}
