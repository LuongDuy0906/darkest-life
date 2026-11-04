using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static event Action<float> OnTakeDamage;

    private PlayerHeatlh playerHeatlh;

    void Awake()
    {
        playerHeatlh = FindAnyObjectByType<PlayerHeatlh>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            playerHeatlh.SetIsTakeDamage(true);
            OnTakeDamage?.Invoke(enemy.Damage);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            playerHeatlh.SetIsTakeDamage(false);
        }
    }
}
