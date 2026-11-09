using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPool> itemPools;

    private ArrowPool _arrowPool;
    private PlayerExp _playerExp;

    void Awake()
    {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
        _playerExp = FindAnyObjectByType<PlayerExp>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
        PlayerCollision.OnTakeItem += HandleItemTaken;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
        PlayerCollision.OnTakeItem -= HandleItemTaken;
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {
        Transform enemyTransform = enemy.transform;
        Enemy destroyedEnemy = enemy.GetComponent<Enemy>();

        foreach(ItemPool itemPool in itemPools)
        {
            Debug.Log(itemPool.ItemData.itemType);
            int numberItemToToReturn = destroyedEnemy.GetNumberItemToReturn(itemPool.ItemData.itemType);
            if(destroyedEnemy.GetItemDrop().Contains(itemPool.ItemData))
            {
                itemPool.GetPool(enemyTransform.position, numberItemToToReturn);
            }
        }
    }

    private void HandleItemTaken(Item item)
    {
        bool itemCollected = false;

        if (!item.gameObject.activeSelf) return;

        ItemPool itemPool = item.GetComponentInParent<ItemPool>();
        if (itemPool == null) return;

        switch (item.ItemType)
        {
            case ItemType.Arrow:
                itemCollected = _arrowPool.AddArrow();
                break;
            case ItemType.Exp:
                _playerExp.EarnedExp(1);
                itemCollected = true;
                break;
        }

        if(itemCollected)
        {
            itemPool.RestorePool(item.gameObject);
        }
    }
}
