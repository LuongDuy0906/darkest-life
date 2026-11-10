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
    private AudioManager _audioManager;

    void Awake()
    {
        arrows = defaultArrow;
        OnArrowsChanged?.Invoke(arrows);
        _audioManager = AudioManager.Instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < defaultArrow; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.GetComponent<ArrowCollision>().Initialize(this, _audioManager);
            arrow.GetComponent<ArrowMovement>().Initialize(this);
            arrow.SetActive(false);
            available.Enqueue(arrow);
        }
    }

    public void GetPool(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (available.Count != 0 && arrows > 0)
        {
            _audioManager.PlayBowShootingSound();
            GameObject arrow = available.Dequeue();
            arrow.transform.position = spawnPosition;
            arrow.transform.rotation = spawnRotation;
            arrow.SetActive(true);
            arrows--;
            OnArrowsChanged?.Invoke(arrows); 
        } else if (arrows <= 0)
        {
            _audioManager.PlayOutOfArrowSound();
        }
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

    public bool AddArrow()
    {
        if(arrows == defaultArrow) return false;
        _audioManager.PlayItemCollectedSound();
        arrows += 1;
        OnArrowsChanged?.Invoke(arrows);
        return true;
    }
}
