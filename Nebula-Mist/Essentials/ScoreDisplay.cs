// (~˘▾˘)~ spaghettiSyntax
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText = null;

    void Update()
    {
        scoreText.text = GameSession.instance.GetScore().ToString();
    }
}
