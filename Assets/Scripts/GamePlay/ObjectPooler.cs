using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnInterval;

    private Queue<GameObject> available = new();

    void Awake()
    {
        for(int i = 0; i < spawnInterval; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            available.Enqueue(enemy);
        }
    }

    public GameObject GetPool(Vector3 spawnPosition)
    {
        if (available.Count == 0) {
            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.SetActive(false);
            available.Enqueue(newEnemy);
        }

        GameObject enemy = available.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);
        return enemy;
    }
}
