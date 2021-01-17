// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip[] bgmArray = null;
    [SerializeField] private AudioSource audioSource;

    private string currentScene;
    private bool startMenuBGMStarted = false;
    private bool optionsMenuBGMStarted = false;
    private bool battleSceneBGMStarted = false;
    private bool endGameSceneBGMStarted = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            audioSource.volume = PlayerPrefsController.GetMasterVolume();
        }
        else
        {
            PlayerPrefsController.SetMasterVolume(0.1f);
            Start();
        }
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name.ToString();
        CheckIfOnStartMenu();
        CheckIfOnOptionsMenu();
        CheckIfOnBattleScene();
        CheckIfOnEndGameScene();
    }

    private void CheckIfOnStartMenu()
    {
        if (currentScene.Equals("0_StartMenu"))
        {
            if (!startMenuBGMStarted)
            {
                PlayStartMenuClip();
            }            
        }
        else
        {
            startMenuBGMStarted = false;
        }
    }

    private void PlayStartMenuClip()
    {
        startMenuBGMStarted = true;
        //audioSource.Stop();
        audioSource.clip = bgmArray[3];
        audioSource.Play();
    }

    private void CheckIfOnOptionsMenu()
    {
        if (currentScene.Equals("8_OptionsMenu"))
        {
            if (!optionsMenuBGMStarted)
            {
                PlayOptionsMenuClip();
            }            
        }
        else
        {
            optionsMenuBGMStarted = false;
        }
    }

    private void PlayOptionsMenuClip()
    {
        optionsMenuBGMStarted = true;
        //audioSource.Stop();
        audioSource.clip = bgmArray[0];
        audioSource.Play();
    }

    private void CheckIfOnBattleScene()
    {
        if (currentScene.Equals("2_BattleScene"))
        {
            if (!battleSceneBGMStarted)
            {
                PlayBattleSceneClip();
            }            
        }
        else
        {
            battleSceneBGMStarted = false;
        }
    }

    private void PlayBattleSceneClip()
    {
        battleSceneBGMStarted = true;
        //audioSource.Stop();
        audioSource.clip = bgmArray[2];
        audioSource.Play();
    }

    private void CheckIfOnEndGameScene()
    {
        if (currentScene.Equals("9_EndGameScene"))
        {
            if (!endGameSceneBGMStarted)
            {
                PlayEndGameSceneClip();
            }            
        }
        else
        {
            endGameSceneBGMStarted = false;
        }
    }

    private void PlayEndGameSceneClip()
    {
        endGameSceneBGMStarted = true;
        //audioSource.Stop();
        audioSource.clip = bgmArray[1];
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
