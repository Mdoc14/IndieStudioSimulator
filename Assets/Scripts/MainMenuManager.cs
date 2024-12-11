using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private TextMeshProUGUI speedText;
    public static MainMenuManager Instance; 

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void TogglePause(bool pause)
    {
        pauseMenu.SetActive(pause);
        settings.SetActive(false);
        if (pause) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        _mixer.SetFloat("Volume", volume);
    }

    public void SetSensMultiplier(float sens)
    {
        CameraRotation.sensMultiplier = sens;
    }

    public void SetSpeedText(string text)
    {
        speedText.text = $"Velocidad: x{text}";
    }
}
