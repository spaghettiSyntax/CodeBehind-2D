// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    private const string DIFFICULTY_KEY = "Difficulty";
    private const float MIN_DIFFICULTY = 0f;
    private const float MAX_DIFFICULTY = 2f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME
            && volume <= MAX_VOLUME)
        {
            Debug.Log("Master volume set to " + volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range.");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY); 
    }

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY
            && difficulty <= MAX_DIFFICULTY)
        {
            Debug.Log("Difficulty has been set to " + difficulty);
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
            Debug.Log("PlayerPrefs.GetFloat: " + PlayerPrefs.GetFloat(DIFFICULTY_KEY));
        }
        else
        {
            Debug.LogError("Difficulty setting is not in range.");
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
}
