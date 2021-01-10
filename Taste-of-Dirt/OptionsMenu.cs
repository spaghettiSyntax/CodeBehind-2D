// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio Section")]
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private Slider mainSlider = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;
    [SerializeField] private AudioSource sfxLoop = null;

    // Audio
    public void SetMainVolume()
    {
        audioMixer.SetFloat("MainVolume", mainSlider.value);

        // Save user's preferences
        PlayerPrefs.SetFloat("MainVolume", mainSlider.value);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicSlider.value);

        // Save user's preferences
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);

        // Save user's preferences
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void PlaySFXLoop()
    {
        sfxLoop.Play();
    }

    public void StopSFXLoop()
    {
        sfxLoop.Stop();
    }
}
