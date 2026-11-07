using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject itemPrefab;

    public ItemData ItemData => itemData;
    private int _defaultItemDrop = 20;
    private Queue<GameObject> availableItem = new();
    private float minDistance = 1f;
    private float maxDistance = 1f;

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

    public void GetPool(Vector3 spawnPosition, int spawnInterval)
    {
        while(spawnInterval > 0 && availableItem.Count > 0) {
            Vector3 randomPosition = RandomSpawnPosition(spawnPosition);
            GameObject item = availableItem.Dequeue();
            item.transform.position = randomPosition;
            item.SetActive(true);
            Debug.Log(spawnInterval);
            spawnInterval--;
        }
    }

    public void RestorePool(GameObject item)
    {
        if (!item.activeSelf) return;

        item.SetActive(false);
        availableItem.Enqueue(item);
    }

        private Vector3 RandomSpawnPosition(Vector3 enemyPosition)
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
