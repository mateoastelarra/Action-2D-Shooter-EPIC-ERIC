using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping = true;
    [SerializeField] float timeUpdateSpeed = 0.1f;
    WaveConfigSO currentWave;
    
    void Start()
    {
        StartCoroutine("SpawnEnemyWaves");
    }

    void Update() 
    {
        UpdateTimeBetweenWaves();
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO waveConfig in waveConfigs)
            {
                currentWave = waveConfig;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), 
                            currentWave.GetFirstWaypoint().position,
                            Quaternion.identity,
                            transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            } 
        }
        while(isLooping);   
    }

    void UpdateTimeBetweenWaves()
    {
        float test = Random.Range(0,4);
        if (timeBetweenWaves > 1)
        {
            if (test > 0)
            {
                timeBetweenWaves -= Time.deltaTime * timeUpdateSpeed;
            }
            else
            {
                timeBetweenWaves += Time.deltaTime * timeUpdateSpeed;
            }
        }
        else
        {
            timeBetweenWaves += Random.Range(1,2);
        }
    }
}
