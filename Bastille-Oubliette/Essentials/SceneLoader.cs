// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private float timeToWait = 2.5f;
    private int currentSceneIndex;
    private string backFromOptionsScene = null;
    private string currentScene;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (currentSceneIndex == 0)
        {
            EnterScene();
        }

        if (SceneManager.GetActiveScene().name.Equals("1_LoadingScreen"))
        {
            StartCoroutine(WaitForSimulatedLoadTime());
        }
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name.ToString();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentScene.Equals("0_StartMenu"))
        {
            bool checkIfKey = PlayerPrefs.HasKey("Difficulty");
            if (!checkIfKey)
            {
                PlayerPrefsController.SetDifficulty(1f);
            }
        }
    }


    public void EnterScene()
    {
        //SetRawImageAlphaToOne();
        SceneFader.instance.fadeSpeed = 1f;
        StartCoroutine(FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.IntoScene));
    }

    public void ExitScene()
    {
        SceneFader.instance.fadeSpeed = 1f;
        StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadSceneByBuildIndex(SceneFader.FadeDirection.OutOfScene, currentSceneIndex + 1));
    }

    public void ExitScene(string sceneStringToLoad)
    {
        SceneFader.instance.fadeSpeed = 1f;
        StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadSceneByStringName(SceneFader.FadeDirection.OutOfScene, sceneStringToLoad));
    }

    private static void SetRawImageAlphaToOne()
    {
        SceneFader.instance.fadeOutUIImage.canvasRenderer.SetAlpha(255);
    }

    private static void SetRawImageAlphaToZero()
    {
        SceneFader.instance.fadeOutUIImage.canvasRenderer.SetAlpha(0);
    }

    private IEnumerator WaitForSimulatedLoadTime()
    {
        SceneFader.instance.fadeSpeed = 1f;
        yield return new WaitForSeconds(timeToWait);        
        ExitScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
        EnterScene();
    }

    public void LoadStartMenu()
    {
        LevelLoaderController.instance.levelSelected = "";
        ExitScene("0_StartMenu");
    }

    public void LoadCurrentScene()
    {
        ExitScene("2_BattleScene");
    }

    public void LoadWinScene()
    {
        ExitScene("9_EndGameScene");
    }

    public void LoadLoseScene()
    {
        ExitScene("9_EndGameScene");
    }

    public void LoadOptionsScene()
    {
        backFromOptionsScene = SceneManager.GetActiveScene().name.ToString();
        SceneManager.LoadScene("8_OptionsMenu");
    }

    public void LoadBackFromOptionsScene()
    {
        FindObjectOfType<OptionsController>().SaveAndExit();

        if (backFromOptionsScene != null)
        {
            SceneManager.LoadScene(backFromOptionsScene);
        }
        else
        {
            SceneManager.LoadScene("0_StartMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
