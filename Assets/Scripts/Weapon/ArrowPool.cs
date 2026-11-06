using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static event Action<int> OnArrowsChanged; 

    [SerializeField] private GameObject arrowPrefab;
    private int defaultArrow = 30;

    private Queue<GameObject> available = new();

    public float ArrowDamage = 1f;
    public float ArrowSpeed = 5f;
    private int arrows;

    void Awake()
    {
        arrows = defaultArrow;
        OnArrowsChanged?.Invoke(arrows);
    }

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
        if (available.Count != 0 && arrows > 0)
        {
            GameObject arrow = available.Dequeue();
            arrow.transform.position = spawnPosition;
            arrow.transform.rotation = spawnRotation;
            arrow.SetActive(true);
            arrows--;
            OnArrowsChanged?.Invoke(arrows);
            return arrow;
        }

        return null;
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
