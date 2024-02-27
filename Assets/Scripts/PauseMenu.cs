using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private Selectable firstSettingsSelected;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingsMenuUI;

    void Start()
    {
        Resume();
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (SettingsMenuUI != null) SettingsMenuUI.SetActive(false);
    }
    
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (firstSelected != null)
        {
            firstSelected.Select();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        SettingsDisplayMode(true);
    }

    public void CloseSettings()
    {
        SettingsDisplayMode(false);
    }

    private void SettingsDisplayMode(bool settingsOn)
    {
        Selectable selectable = settingsOn ? firstSettingsSelected : firstSelected;
        if (selectable != null)
        {
            selectable.Select();
        }

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(!settingsOn);
        }

        if (SettingsMenuUI != null)
        {
            SettingsMenuUI.SetActive(settingsOn);
        }
    }
}
