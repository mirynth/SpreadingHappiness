using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private Selectable firstSelected;
    Resolution[] resolution;
    public AudioMixer audioMixer;

    private void Start()
    {
        resolution = Screen.resolutions.Where(resolution => resolution.refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value).ToArray();
        Screen.SetResolution(resolution[PlayerPrefs.GetInt("resolution")].width, resolution[PlayerPrefs.GetInt("resolution")].height, Screen.fullScreen);
        audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        if (PlayerPrefs.GetString("fullscreen").ToLower() == "false")
            Screen.fullScreen = false;
        else
            Screen.fullScreen = true;
        if (firstSelected != null)
        {
	        firstSelected.Select();
        }
    }

    public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void QuitGame()
    {
        #if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {   
                UnityEditor.EditorApplication.isPlaying = false;
            }
        #else
            Application.Quit();
        #endif
    }
}

