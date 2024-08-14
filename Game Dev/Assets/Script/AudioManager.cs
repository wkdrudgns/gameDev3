using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static string VolumeKey = "Volume";
    private const string VolumeParameterName = "MasterVolume";
    private const float DefaultVolume = 0.5f;
    private const float MinVolume = 0.001f;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, DefaultVolume);
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            float clampedVolume = Mathf.Clamp(volume, MinVolume, 1.0f);
            float dbVolume = Mathf.Lerp(-80, 0, clampedVolume);
            audioMixer.SetFloat(VolumeParameterName, dbVolume);
        }
        else
        {
            Debug.LogWarning("AudioMixer is not assigned.");
        }

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}
