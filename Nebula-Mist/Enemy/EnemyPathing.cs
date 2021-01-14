// (~˘▾˘)~ spaghettiSyntax
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private List<Transform> waypointsList = null;
    private SO_WaveConfig waveConfig = null;
    private int waypointIndex = 0;

    void Start()
    {
        waypointsList = waveConfig.GetWaypoints();
        transform.position = waypointsList[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypointsList.Count - 1)
        {
            Vector3 targetPosition = waypointsList[waypointIndex].transform.position;
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(SO_WaveConfig currentWave)
    {
        this.waveConfig = currentWave;
    }
}
