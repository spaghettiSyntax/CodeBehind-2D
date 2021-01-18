// (~˘▾˘)~ spaghettiSyntax
// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Sprite[] heartArray = null;
    [SerializeField] private Image heartImage = null;

    // State
    public int playerHealth = 3;
    public bool playerJustDamaged = false;
    //private bool playingHurtAnimation = false;

    // Methods
    private void Awake()
    {
        // Singleton Pattern
        int numGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        playerHealth = 3;
        UpdatePlayerHeart();
    }

    public void ManagePlayerDamage()
    {        
        if (playerHealth > 1)
        {
            if (playerJustDamaged) { return; }
            DamagePlayer();
        }
        else
        {
            DamagePlayer();
            GameOver();
        }
    }

    public void HeartPickup()
    {
        if (playerHealth + 1 <= 3)
        {
            playerHealth++;
        }
        UpdatePlayerHeart();
    }

    private void DamagePlayer()
    {
        playerHealth--;
        UpdatePlayerHeart();
        StartCoroutine(PlayerInvincibleAfterDamage());
        StartCoroutine(HurtAnimationExitTimer());
    }

    private IEnumerator HurtAnimationExitTimer()
    {
        FindObjectOfType<Player>().animator.SetBool("playingHurtAnimation", true);
        yield return new WaitForSecondsRealtime(0.15f);
        FindObjectOfType<Player>().animator.SetBool("playingHurtAnimation", false);
    }

    private IEnumerator PlayerInvincibleAfterDamage()
    {
        playerJustDamaged = true;
        FindObjectOfType<Player>().animator.SetBool("justDamaged", true);
        yield return new WaitForSecondsRealtime(2f);
        playerJustDamaged = false;
        FindObjectOfType<Player>().animator.SetBool("justDamaged", false);
    }

    private void GameOver()
    {
        FindObjectOfType<Player>().Die();
        StartCoroutine(SlowMotionDeath());
    }

    private IEnumerator SlowMotionDeath()
    {
        // Slow Motion Effect
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1f);
        // TODO: Create GameOver screen.
        Time.timeScale = 1f;
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }

    private void UpdatePlayerHeart()
    {
        switch (playerHealth)
        {
            case 3:
                heartImage.sprite = heartArray[3];
                break;
            case 2:
                heartImage.sprite = heartArray[2];
                break;
            case 1:
                heartImage.sprite = heartArray[1];
                break;
            case 0:
                heartImage.sprite = heartArray[0];
                break;
            default:
                break;
        }
    }
}
