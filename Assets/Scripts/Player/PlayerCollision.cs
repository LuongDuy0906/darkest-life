using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static event Action<float> OnTakeDamage;

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            OnTakeDamage?.Invoke(enemy.Damage);
        }
    }
}
