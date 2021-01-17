using System.Collections;
using UnityEngine;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] private GameObject battleCompleteOverlay = null;
    [SerializeField] private GameObject battleLoseOverlay = null;
    [SerializeField] private float waitToLoad = 3f;

    private int numberOfEnemies = 0;
    private bool levelTimerFinished = false;

    private void Start()
    {
        battleCompleteOverlay.SetActive(false);
        battleLoseOverlay.SetActive(false);
    }

    public void EnemySpawned()
    {
        numberOfEnemies++;
    }

    public void EnemyDestroyed()
    {
        numberOfEnemies--;

        if (!FindObjectOfType<LoseCondition>().gameLost)
        {
            if (numberOfEnemies <= 0
                && levelTimerFinished)
            {
                StartCoroutine(HandleWinCondition());
            }
        }
    }

    IEnumerator HandleWinCondition()
    {
        battleCompleteOverlay.SetActive(true);
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<SceneLoader>().LoadWinScene();
    }

    public void HandleLoseCondition()
    {
        battleLoseOverlay.SetActive(true);
        StopSpawners();
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        EnemySpawner[] enemySpawnerArray = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner enemySpawner in enemySpawnerArray)
        {
            enemySpawner.StopSpawning();
        }
    }
}
