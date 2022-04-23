using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigurations = new List<WaveConfig>();
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool looping = false;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigurations.Count; ++waveIndex)
        {
            WaveConfig currentWave = waveConfigurations[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfiguration)
    {
        if (waveConfiguration != null)
        {
            for (int enemyCount = 0; enemyCount < waveConfiguration.GetNumberOfEnemies(); enemyCount++)
            {
                GameObject enemy = Instantiate(waveConfiguration.GetEnemyPrefab(), waveConfiguration.GetWaypoints()[0].position, Quaternion.identity);
                enemy.GetComponent<EnemyPathing>().SetWaveConfiguration(waveConfiguration);

                yield return new WaitForSeconds(waveConfiguration.GetTimeBetweenSpawns() + Random.Range(0.0f, waveConfiguration.GetSpawnRandomFactor()));
            }
        }
    }
}
