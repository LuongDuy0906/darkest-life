using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject itemPrefab;

    public ItemData ItemData => itemData;
    private int _defaultItemDrop = 20;
    private readonly Queue<GameObject> availableItem = new();
    private float minDistance = 1f;
    private float maxDistance = 1f;

    void Start()
    {
        for(int i = 0; i <_defaultItemDrop; i++)
        {
            GameObject item = Instantiate(itemPrefab, transform);
            item.SetActive(false);
            availableItem.Enqueue(item);
        }
    }

    public GameObject GetPool(Vector3 worldPosition)
    {
        GameObject item = availableItem.Count > 0
            ? availableItem.Dequeue()
            : Instantiate(itemPrefab, transform);

        item.SetActive(false);
        item.transform.position = worldPosition;
        item.SetActive(true);
        return item;
    }

    public void RestorePool(GameObject item)
    {
        if (!item.activeSelf) return;

        item.SetActive(false);
        availableItem.Enqueue(item);
    }

    public Vector3 RandomSpawnPosition(Vector3 enemyPosition)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPosition = enemyPosition + new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0f
        ) * distance;

        return spawnPosition;
    }
}
