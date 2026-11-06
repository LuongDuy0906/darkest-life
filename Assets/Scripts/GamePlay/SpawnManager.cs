using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPooler orcPool;
    [SerializeField] private ObjectPooler bloodMonsterPool;
    [SerializeField] private ObjectPooler demonPool;

    private float minDistance = 8f;
    private float maxDistance = 12f;
    private Transform playerPosition;
    private float delaySpawnTime;
    private float defaultDelaySpawnTime = 1f;
    private GameManager _gameManager;
    private List<ObjectPooler> poolToSpawn = new();

    void Awake()
    {
        playerPosition = FindAnyObjectByType<PlayerMovement>().transform;
        delaySpawnTime = defaultDelaySpawnTime;
        _gameManager = FindAnyObjectByType<GameManager>();

        poolToSpawn.Add(orcPool);
    }

    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
        Enemy.OnEnemyDisappear += HandleEnemyDestroy;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
        Enemy.OnEnemyDisappear -= HandleEnemyDestroy;
    }

    void Update()
    {
        if(_gameManager.GetGameState() != GameState.Playing) return;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if((_gameManager.CurrentLevel >= 5 || _gameManager.TimeSurvived >= 60f) && !poolToSpawn.Contains(bloodMonsterPool))
        {
            poolToSpawn.Add(bloodMonsterPool);
        }

        if((_gameManager.CurrentLevel >= 10 || _gameManager.TimeSurvived >= 120f) && !poolToSpawn.Contains(demonPool))
        {
            poolToSpawn.Add(demonPool);
        }

        if(delaySpawnTime > 0f)
        {
            delaySpawnTime -= Time.deltaTime;
        } else
        {
            foreach(ObjectPooler pool in poolToSpawn)
            {
                Vector3 randomSpawnPosition = RandomSpawnPosition();
                pool.GetPool(randomSpawnPosition);
                delaySpawnTime = defaultDelaySpawnTime;
            }
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPosition = playerPosition.position + new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0f
        ) * distance;

        return spawnPosition;
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {   
        ObjectPooler enemyPool = enemy.GetComponentInParent<ObjectPooler>();

        enemyPool.RestorePool(enemy);
    }
}
