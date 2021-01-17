// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider = null;
    [SerializeField] float defaultVolume = 0.2f;
    [SerializeField] Slider difficultySlider = null;
    [SerializeField] float defaultDifficulty = 1f;

    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    void Update()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager)
        {
            audioManager.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No audio manager found. Must start game from start menu.");
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetDifficulty(difficultySlider.value);
    }

    public void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
    }
}
