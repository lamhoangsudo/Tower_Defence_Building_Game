using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyWaveManager : MonoBehaviour
{
    public enum State
    {
        WaitingToSpawnWave,
        SpawningWave,
    }
    public event EventHandler OnNumberWaveChange;
    public int wave {  get; private set; }
    private int numberEnemyWaveIncreases = 2;
    public float timeToSpawn {  get; private set; }
    private float timeToWaitSpawnNextWavel = 10f;
    public float timeToSpawnEnenmy {  get; private set; }
    private float timeToWaitSpawnNextEnemy;
    private int numberOfEnemiesInWave;
    private State state;
    [SerializeField]
    private List<Transform> ListSpawnPositionTransform;
    public Transform spawnPositionTransform {  get; private set; }
    public static EnemyWaveManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        state = State.WaitingToSpawnWave;
        timeToSpawn = 3f;
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
                    SpawnEnemy(ListSpawnPositionTransform, 3f, 10);
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
                    timeToSpawn = timeToWaitSpawnNextWavel;
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
        OnNumberWaveChange?.Invoke(this, EventArgs.Empty);
    }
}
