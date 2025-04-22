using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] WaveConfigSO currentWave;
    [SerializeField] float timeBetweenWaves = 2f;
    [SerializeField] bool isLooping = true;
    // Start is called before the first frame update
    void Start()
    {
        // SpawnEnemy();
        StartCoroutine(SpawnEnemyWaves()); //start coroutine to spawn enemies
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave; //set the current wave in the loop
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                    currentWave.GetEnemyPrefab(i),
                    currentWave.GetWaypoints()[0].position,
                    // Quaternion.identity,
                    Quaternion.Euler(0, 0, 180),
                    transform
                    ); //*Instantiate the enemy prefab at the first waypoint position
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                    //Wait for the spawn time before spawning the next enemy
                }//For Loop Ends
                yield return new WaitForSeconds(timeBetweenWaves);
            }//Foreach Loop Ends
        } while (isLooping);
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave; //*return the current wave config
    }

    // void SpawnEnemy()
    // {

    //     foreach (WaveConfigSO wave in waveConfigs)
    //     {
    //         currentWave = wave;
    //         for (int i = 0; i < currentWave.GetEnemyCount(); i++)
    //         {
    //             Instantiate(
    //             currentWave.GetEnemyPrefab(i),
    //             currentWave.GetWaypoints()[0].position, Quaternion.identity
    //             );
    //             // yield return new WaitForSeconds(currentWave.GetSpawnTime());
    //             //Wait for the spawn time before spawning the next enemy
    //         }
    //     }
}
