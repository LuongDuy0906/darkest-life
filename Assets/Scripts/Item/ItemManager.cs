using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPool> itemPools;

    private ArrowPool _arrowPool;

    void Awake()
    {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
        PlayerCollision.OnTakeItem += HandleItemTaked;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
        PlayerCollision.OnTakeItem -= HandleItemTaked;
    }

    private void HandleEnemyDestroy(GameObject enemy)
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

    private void HandleItemTaked(Item item)
    {
        if (!item.gameObject.activeSelf) return;

        ItemPool itemPool = item.GetComponentInParent<ItemPool>();
        if (itemPool == null) return;

        switch (item.ItemType)
        {
            case ItemType.Arrow:
                _arrowPool.AddArrow();
                break;
        }

        itemPool.RestorePool(item.gameObject);
    }
}
