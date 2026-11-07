using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static event Action<float> OnTakeDamage;
    public static event Action<Item> OnTakeItem;

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            OnTakeDamage?.Invoke(enemy.Damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Item>(out var item))
        {
            OnTakeItem?.Invoke(item);
        }
    }
}
