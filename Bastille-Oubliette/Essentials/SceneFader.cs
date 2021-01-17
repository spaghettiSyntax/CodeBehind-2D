// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    public RawImage fadeOutUIImage = null;
    [HideInInspector] public float fadeSpeed = 0f;

    public enum FadeDirection
    {
        OutOfScene, // Alpha = 1
        IntoScene // Alpha = 0
    }

    private void Awake()
    {
        instance = this;
        //fadeOutUIImage = FindObjectOfType<RawImage>();
    }

    void OnEnable()
    {
        StartCoroutine(Fade(FadeDirection.IntoScene));
    }

    public IEnumerator Fade(FadeDirection fadeDirection)
    {
        //float currentAlpha = fadeOutUIImage.canvasRenderer.GetAlpha();

        //if (fadeDirection == FadeDirection.In)
        //{
        //    fadeOutUIImage.canvasRenderer.SetAlpha(0);
        //}
        //else if (fadeDirection == FadeDirection.Out)
        //{
        //    fadeOutUIImage.canvasRenderer.SetAlpha(1);
        //}

        float alpha = (fadeDirection == FadeDirection.IntoScene) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.IntoScene) ? 0 : 1;
        if (fadeDirection == FadeDirection.IntoScene)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            // Why were we disabling fadeOutUIImage? To avoid RayCast target perhaps?
            //fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    public IEnumerator FadeAndLoadSceneByStringName(FadeDirection fadeDirection, string sceneStringToLoad)
    {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneStringToLoad);
    }

    public IEnumerator FadeAndLoadSceneByBuildIndex(FadeDirection fadeDirection, int sceneIndexToLoad)
    {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneIndexToLoad);
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.IntoScene) ? -1 : 1);
    }
}
