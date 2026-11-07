using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPool> itemPools;

    private float minDistance = 1f;
    private float maxDistance = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemeDestroy;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemeDestroy;
    }

    private void HandleEnemeDestroy(GameObject enemy)
    {
        Transform enemyTransform = enemy.transform;
        Enemy destroyedEnemy = enemy.GetComponent<Enemy>();

        foreach(ItemPool itemPool in itemPools)
        {
            Vector3 randomPosition = RandomSpawnPosition(enemyTransform);
            int numberItemToToReturn = destroyedEnemy.GetNumberItemToReturn(itemPool.ItemData.itemType);
            if(destroyedEnemy.GetItemDrop().Contains(itemPool.ItemData))
            {

                itemPool.GetPool(randomPosition, numberItemToToReturn);
            }
        }
    }

    private Vector3 RandomSpawnPosition(Transform enemyTransform)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPosition = enemyTransform.position + new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0f
        ) * distance;

        return spawnPosition;
    }
}
