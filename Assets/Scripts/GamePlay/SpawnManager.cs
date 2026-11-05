using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ObjectPooler orcPool;
    private float minDistance = 8f;
    private float maxDistance = 12f;
    private Transform playerPosition;
    private float delaySpawnTime;
    private float defaultDelaySpawnTime = 1f;
    private GameManager _gameManager;

    void Awake()
    {
        orcPool = GetComponentInChildren<ObjectPooler>();
        playerPosition = FindAnyObjectByType<PlayerMovement>().transform;
        delaySpawnTime = defaultDelaySpawnTime;
        _gameManager = FindAnyObjectByType<GameManager>();
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
        if(delaySpawnTime > 0f)
        {
            delaySpawnTime -= Time.deltaTime;
        } else
        {
            Vector3 ramdonSpawnPosition = RandomSpawnPosition();
            orcPool.GetPool(ramdonSpawnPosition);
            delaySpawnTime = defaultDelaySpawnTime;
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
        orcPool.RestorePool(enemy);
    }
}
