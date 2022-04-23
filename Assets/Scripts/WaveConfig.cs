using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2.0f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    
    public List<Transform> GetWaypoints()
    {
        List<Transform> waveWaypoints = new List<Transform>();

        if (pathPrefab != null)
        {
            foreach (Transform child in pathPrefab.transform)
            {
                waveWaypoints.Add(child);
            }
        }

        return waveWaypoints; 
    }
    
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    
    public float GetMoveSpeed() { return moveSpeed; }
}
