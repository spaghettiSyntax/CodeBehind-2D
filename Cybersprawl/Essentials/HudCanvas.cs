// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class HudCanvas : MonoBehaviour
{
    private void Awake()
    {
        // Singleton Pattern
        int numHudCanvas = FindObjectsOfType<HudCanvas>().Length;
        if (numHudCanvas > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
