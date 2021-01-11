// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // Serialized For Debugging Purposes
    [SerializeField] private int breakableBlocks;

    private void Awake()
    {
        instance = this;
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            SceneLoader.instance.LoadNextScene();
        }
    }
}