// (~˘▾˘)~ spaghettiSyntax
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;

    [SerializeField] private GameObject bgmController = null;
    [SerializeField] private List<AudioClip> bgmSelection = null;
    [SerializeField] private GameObject audioOptions = null;
    [SerializeField] private TMP_Dropdown bgmDropdownSelector = null;

    private int chosenBGM = 0;

    private void Awake()
    {
        instance = this;

        int audioControllerCount = FindObjectsOfType<AudioController>().Length;
        if (audioControllerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        BGMusicChoice();
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name.Equals("0_StartMenu"))
        {
            audioOptions.SetActive(true);
        }
        else
        {
            audioOptions.SetActive(false);
        }
    }

    public void BGMusicChoice()
    {
        bgmController.GetComponent<AudioSource>().Stop();
        chosenBGM = bgmDropdownSelector.value;
        int choiceTest = bgmDropdownSelector.value;
        Debug.Log(choiceTest);
        bgmController.GetComponent<AudioSource>().PlayOneShot(bgmSelection[chosenBGM]);
    }
}
