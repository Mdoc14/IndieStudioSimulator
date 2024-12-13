using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI endText;
    public static MainMenuManager Instance; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
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
        GameObject.FindObjectOfType<PlayerMovement>().canMove = !pause;
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

    public void ToggleCollisions(bool c)
    {
        PlayerMovement.collides = c;
        PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
        if (player) player.ToggleCollision();
    }

    public void GameEnded(bool success)
    {
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        endMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        if (!success) endText.text = "La empresa ha entrado en <color=red>bancarrota</color>";
        else endText.text = "El último juego ha sido un éxito. La empresa concede <color=blue>vacaciones</color>";
    }
}
