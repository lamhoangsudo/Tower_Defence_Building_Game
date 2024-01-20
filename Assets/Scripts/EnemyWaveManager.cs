using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public enum State
    {
        WaitingToSpawnWave,
        SpawningWave,
    }
    private int wave;
    private int numberEnemyWaveIncreases = 2;
    private float timeToSpawn;
    private float timeToWaitSpawnNextWavel = 10f;
    private float timeToSpawnEnenmy;
    private float timeToWaitSpawnNextEnemy;
    private int numberOfEnemiesInWave;
    private State state;
    [SerializeField]
    private List<Transform> ListSpawnPositionTransform;
    private Transform spawnPositionTransform;
    private void Start()
    {
        state = State.WaitingToSpawnWave;
        wave = 0;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnWave:
                timeToSpawn -= Time.deltaTime;
                if (timeToSpawn <= 0)
                {
                    timeToSpawn = timeToWaitSpawnNextWavel;
                    SpawnEnemy(ListSpawnPositionTransform, 2f, 10);
                }
                break;
            case State.SpawningWave:
                timeToSpawnEnenmy -= Time.deltaTime;
                if (numberOfEnemiesInWave > 0)
                {
                    if (timeToSpawnEnenmy <= 0)
                    {
                        timeToSpawnEnenmy = timeToWaitSpawnNextEnemy;
                        EnemySystem.CreateEnemy(spawnPositionTransform.position + UtilClass.GetRamdomVector() * Random.Range(0f, 10f));
                        numberOfEnemiesInWave--;
                    }
                }
                else
                {
                    state = State.WaitingToSpawnWave;
                }
                break;
        }
    }
    private void SpawnEnemy(List<Transform> spawnPositionTransform, float timeTotalToSpawnAllEnemy, int numberOfEnemiesInWave)
    {
        this.spawnPositionTransform = spawnPositionTransform[Random.Range(0, spawnPositionTransform.Count)];       
        this.numberOfEnemiesInWave = numberOfEnemiesInWave + numberEnemyWaveIncreases * wave;
        this.timeToWaitSpawnNextEnemy = timeTotalToSpawnAllEnemy / numberOfEnemiesInWave;      
        wave++;
        state = State.SpawningWave;
    }
}
