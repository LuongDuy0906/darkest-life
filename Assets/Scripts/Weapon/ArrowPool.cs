using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    private int defaultArrow = 5;

    private Queue<GameObject> available = new();

    public float ArrowDamage = 1f;
    public float ArrowSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < defaultArrow; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.GetComponent<ArrowCollision>().Initialize(this);
            arrow.GetComponent<ArrowMovement>().Initialize(this);
            arrow.SetActive(false);
            available.Enqueue(arrow);
        }
    }

    public GameObject GetPool(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (available.Count == 0) {
            GameObject newArrow = Instantiate(arrowPrefab, transform);
            newArrow.GetComponent<ArrowCollision>().Initialize(this);
            newArrow.GetComponent<ArrowMovement>().Initialize(this);
            newArrow.SetActive(false);
            available.Enqueue(newArrow);
        }

        GameObject arrow = available.Dequeue();
        arrow.transform.position = spawnPosition;
        arrow.transform.rotation = spawnRotation;
        arrow.SetActive(true);
        return arrow;
    }

    public void RestorePool(GameObject arrow)
    {
        if (!arrow.activeSelf) return;
        
        arrow.SetActive(false);
        available.Enqueue(arrow);
    }

    public void UpdateArrowSpeed(float speed)
    {
        ArrowSpeed *= speed;
    }

    public void UpgradeArrowDamage(float damage)
    {
        ArrowDamage *= damage;
    }
}
