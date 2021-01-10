// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer = null;

    void Start()
    {
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            mainMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("MainVolume"));
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            mainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            mainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        }
    }
}
