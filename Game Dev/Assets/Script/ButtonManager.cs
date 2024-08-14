using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject RuleText;
    public GameObject SoundSettingsPanel;
    public Slider volumeSlider;

    private void Start()
    {
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

    public void OnClickStart()
    {
        Debug.Log("Game Start");
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickTutorial()
    {
        Debug.Log("Moving to Tutorial Scene");
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickRule()
    {
        if (RuleText != null)
        {
            bool isActive = RuleText.activeSelf;
            RuleText.SetActive(!isActive);
            Debug.Log($"RuleText panel has been {(RuleText.activeSelf ? "activated" : "deactivated")}.");
        }
        else
        {
            Debug.LogWarning("RuleText is null.");
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

    public void OnQuitButtonClick()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
