using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPool> itemPools;
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
            int numberItemToToReturn = destroyedEnemy.GetNumberItemToReturn(itemPool.ItemData.itemType);
            if(destroyedEnemy.GetItemDrop().Contains(itemPool.ItemData))
            {

                itemPool.GetPool(enemyTransform.position, numberItemToToReturn);
            }
        }
    }
}
