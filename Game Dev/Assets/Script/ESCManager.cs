using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour
{

    public GameObject SoundSettingsPanel;
    public Slider volumeSlider;
    public AudioManager audioManager;
    
    public GameObject menuUI;
    public Button returnToMenuButton;
    public string menuSceneName = "MenuScene";
    public bool isPaused = false;

    private void Start()
    {
        menuUI.SetActive(false);

        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonClicked);
        }

        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(AudioManager.VolumeKey, 0.5f);
            volumeSlider.value = savedVolume;
            volumeSlider.minValue = 0.01f;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        else
        {
            Debug.LogWarning("VolumeSlider is not assigned.");
        }

        InitializeAudioManager();
    }

    private void InitializeAudioManager()
    {
        if (AudioManager.Instance != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(AudioManager.VolumeKey, 0.5f);
            AudioManager.Instance.SetVolume(savedVolume);
        }
        else
        {
            Debug.LogWarning("AudioManager instance is not available.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnClickSoundSettings()
    {
        if (SoundSettingsPanel != null)
        {
            bool isActive = SoundSettingsPanel.activeSelf;
            SoundSettingsPanel.SetActive(!isActive);
            Debug.Log($"SoundSettingsPanel has been {(SoundSettingsPanel.activeSelf ? "activated" : "deactivated")}.");

            if (SoundSettingsPanel.activeSelf)
            {
                Slider slider = SoundSettingsPanel.GetComponentInChildren<Slider>();
                if (slider != null)
                {
                    float savedVolume = PlayerPrefs.GetFloat(AudioManager.VolumeKey, 0.5f);
                    slider.value = savedVolume;
                    slider.onValueChanged.AddListener(OnVolumeChanged);
                }
                else
                {
                    Debug.LogWarning("VolumeSlider not found in SoundSettingsPanel.");
                }
            }
        }
        else
        {
            Debug.LogWarning("SoundSettingsPanel is null.");
        }
    }

    public void OnVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(value);
        }
        else
        {
            Debug.LogWarning("AudioManager instance is null.");
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void OnReturnToMenuButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }
}
