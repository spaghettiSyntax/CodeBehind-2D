using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private Enemy[] goblinRushArray = null;
    [SerializeField] private Enemy[] orcBattleArray = null;
    [SerializeField] private Enemy[] creatureAttackArray = null;
    
    private Enemy[] enemyPrefabArray = null;

    private bool spawn = true;

    IEnumerator Start()
    {
        FillEnemyArray();

        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnEnemy();
        }
    }

    private void FillEnemyArray()
    {
        if (LevelLoaderController.instance.levelSelected.Equals(""))
        {
            for (int i = 0; i < orcBattleArray.Length; i++)
            {
                enemyPrefabArray[i] = orcBattleArray[i];
            }
        }
        else
        {
            ResizeEnemyArray(LevelLoaderController.instance.levelSelected);
            switch (LevelLoaderController.instance.levelSelected)
            {
                case "GoblinRush":
                    for (int i = 0; i < goblinRushArray.Length; i++)
                    {
                        enemyPrefabArray[i] = goblinRushArray[i];
                    }
                    break;
                case "OrcBattle":
                    for (int i = 0; i < orcBattleArray.Length; i++)
                    {
                        enemyPrefabArray[i] = orcBattleArray[i];
                    }
                    break;
                case "CreatureAttack":
                    for (int i = 0; i < creatureAttackArray.Length; i++)
                    {
                        enemyPrefabArray[i] = creatureAttackArray[i];
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void ResizeEnemyArray(string levelSelected)
    {
        switch (levelSelected)
        {
            case "GoblinRush":
                Array.Resize(ref enemyPrefabArray, goblinRushArray.Length);
                break;
            case "OrcBattle":
                Array.Resize(ref enemyPrefabArray, orcBattleArray.Length);
                break;
            case "CreatureAttack":
                Array.Resize(ref enemyPrefabArray, creatureAttackArray.Length);
                break;
            default:
                break;
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabArray != null)
        {
            int enemyIndex = Random.Range(0, enemyPrefabArray.Length);
            Spawn(enemyPrefabArray[enemyIndex]);
        }
    }

    private void Spawn(Enemy enemy)
    {
        Enemy newEnemy = Instantiate(enemy, transform.position, transform.rotation) as Enemy;
        newEnemy.transform.parent = transform;
    }
}
