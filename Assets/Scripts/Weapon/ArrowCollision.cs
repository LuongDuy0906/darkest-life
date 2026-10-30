using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            Destroy(gameObject);
            enemy.TakeDamage(1);
        }
    }
}
