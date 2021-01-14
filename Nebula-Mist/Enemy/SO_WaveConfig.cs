// (~˘▾˘)~ spaghettiSyntax
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class SO_WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject pathPrefab = null;
    [SerializeField] private List<GameObject> pathPrefabList = null;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;

    private int randomlyChosenPathFromPathList = 0;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    private int GetPath()
    {
        randomlyChosenPathFromPathList = Random.Range(0, pathPrefabList.Count);
        return randomlyChosenPathFromPathList;
    }

    public List<Transform> GetWaypoints() 
    {
        GetPath();
        List<Transform> waveWaypoints = new List<Transform>();

        foreach (Transform transform in pathPrefabList[randomlyChosenPathFromPathList].transform)
        {
            waveWaypoints.Add(transform);
        }

        return waveWaypoints; 
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
