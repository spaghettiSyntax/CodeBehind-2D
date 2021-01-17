// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class LevelLoaderController : MonoBehaviour
{
    public static LevelLoaderController instance;

    public string levelSelected = "";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
