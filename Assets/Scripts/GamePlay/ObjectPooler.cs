using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> available = new();
    private Transform _playerTransform;

    void Awake()
    {
        _playerTransform = FindAnyObjectByType<PlayerMovement>().transform;

        for(int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemy.GetComponent<Enemy>().Initialize(_playerTransform);
            available.Enqueue(enemy);
        }
    }

    public GameObject GetPool(Vector3 spawnPosition)
    {
        if (available.Count != 0) {
            GameObject enemy = available.Dequeue();
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            return enemy;
        }

        return null;
    }

    public void RestorePool(GameObject enemy)
    {
        enemy.SetActive(false);
        available.Enqueue(enemy);
    }
}
