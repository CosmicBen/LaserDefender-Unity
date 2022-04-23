using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig waveConfig = null;
    private List<Transform> waypoints = new List<Transform>();
    private int waypointIndex = 0;

    private void Start()
    {
        if (waveConfig != null)
        {
            waypoints = waveConfig.GetWaypoints();
            transform.position = waypoints[waypointIndex].position;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == waypoints[waypointIndex].position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfiguration(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
