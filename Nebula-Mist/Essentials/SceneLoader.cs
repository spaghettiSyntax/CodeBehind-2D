// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField] private float delayInSeconds = 2f;

    private void Awake()
    {
        instance = this;
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSurvival()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameBeginning()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameMiddle()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGameEnding()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
