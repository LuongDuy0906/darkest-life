using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject itemPrefab;

    public ItemData ItemData => itemData;
    private int _defaultItemDrop = 20;
    private Queue<GameObject> availableItem = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i <_defaultItemDrop; i++)
        {
            GameObject item = Instantiate(itemPrefab, transform);
            item.SetActive(false);
            availableItem.Enqueue(item);
        }
    }

    public GameObject GetPool(Vector3 spawnPosition, int spawnInterval)
    {
        if (availableItem.Count != 0)
        {
            while(spawnInterval > 0) {
                GameObject item = availableItem.Dequeue();
                item.transform.position = spawnPosition;
                item.SetActive(true);
                spawnInterval--;
                return item;
            }
        }

        return null;
    }

    public void RestorePool(GameObject item)
    {
        if (!item.activeSelf) return;

        item.SetActive(false);
        availableItem.Enqueue(item);
    }
}
