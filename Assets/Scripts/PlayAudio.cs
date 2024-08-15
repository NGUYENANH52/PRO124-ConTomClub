using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider totalSlider;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Load saved settings
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f); // Default volume 0.75
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f); // Default volume 0.75
        totalSlider.value = PlayerPrefs.GetFloat("TotalVolume", 0.75f); // Default volume 0.75

        // Add listeners to sliders
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        totalSlider.onValueChanged.AddListener(SetTotalVolume);
    }

    private void SetEffectVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("EffectVolume", volume);
        }
        else
        {
            Debug.LogError("AudioMixer chưa được gán trong PlayAudio script.");
        }
    }

    private void SetMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
        else
        {
            Debug.LogError("AudioMixer chưa được gán trong PlayAudio script.");
        }
    }

    private void SetTotalVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("TotalVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("TotalVolume", volume);
        }
        else
        {
            Debug.LogError("AudioMixer chưa được gán trong PlayAudio script.");
        }
    }

}

/*public class PlayAudio : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}*/

