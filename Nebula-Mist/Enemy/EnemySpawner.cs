// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SO_WaveConfig> waveConfigList = null;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool waveLooping = false;

    // Make your Start() a Coroutine with IEnumerator
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (waveLooping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++)
        {
            SO_WaveConfig currentWave = waveConfigList[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(SO_WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount < currentWave.GetNumberOfEnemies(); enemyCount++)
        {
            GameObject newEnemy = Instantiate(currentWave.GetEnemyPrefab(),
                                              currentWave.GetWaypoints()[0].transform.position,
                                              Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }
}
