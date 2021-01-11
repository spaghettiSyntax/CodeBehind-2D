// (~˘▾˘)~ spaghettiSyntax
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [Range(0.1f, 10.0f)][SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 10;
    [SerializeField] private TMP_Text scoreText = null;
    [HideInInspector][SerializeField] private bool isAutoPlayEnabled = false;
    [SerializeField] private Canvas gameOverCanvas = null;
    [SerializeField] private GameObject gameOverObject = null;
    [SerializeField] private TMP_Text finalScoreText = null;

    [HideInInspector] public bool scoreIsBelowZero = false;

    private Scene currentScene;
    private int currentScore;
    
    private void Awake()
    {
        instance = this;

        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = "Score: " + currentScore.ToString();
        if (currentScore < 0)
        {
            scoreIsBelowZero = true;
        }
        else
        {
            scoreIsBelowZero = false;
        }

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "99_GameOver")
        {
            gameOverObject.SetActive(true);
            UpdateFinalScore();
        }
    }

    private void UpdateFinalScore()
    {
        finalScoreText.text = "Score: " + currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
    }

    public void TakeAwayFromScore()
    {
        currentScore -= 11;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
