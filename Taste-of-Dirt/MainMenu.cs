// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string tasteOfDirtStoryScene = null;
    [SerializeField] private AudioSource mainMenuAudio = null;
    [SerializeField] private GameObject optionsMenu = null;
    [SerializeField] private GameObject aboutMenu = null;
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private GameObject loadingIcon = null;
    [SerializeField] private TMP_Text loadingText = null;

    public void BeginButton()
    {
        StartCoroutine(LoadStart());
    }

    public void OpenOptionsButton()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsButton()
    {
        optionsMenu.SetActive(false);
    }

    public void OpenAboutButton()
    {
        aboutMenu.SetActive(true);
    }

    public void CloseAboutButton()
    {
        aboutMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public IEnumerator LoadStart()
    {
        mainMenuAudio.Stop();
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(tasteOfDirtStoryScene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue.";
                loadingIcon.SetActive(false);

                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                    // Unpause if paused.
                    Time.timeScale = 1f;
                }
            }

            // Prevents infinite loop
            yield return null;
        }
    }
}
