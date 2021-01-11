// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            CannonBallManager.instance.ResetCannonBall();
        }
        else if (collision.CompareTag("NinjaStar"))
        {
            NinjaStarManager.instance.ResetNinjaStar();
        }
        else if (collision.CompareTag("Riceball"))
        {
            RiceballManager.instance.ResetRiceball();
        }

        GameSession.instance.TakeAwayFromScore();
        CheckIfScoreBelowZero();
    }

    private void CheckIfScoreBelowZero()
    {
        if (GameSession.instance.scoreIsBelowZero)
        {
            SceneManager.LoadScene("99_GameOver");
        }
    }
}
