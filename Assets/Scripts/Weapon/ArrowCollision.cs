using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private ArrowPool _arrowPool;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            _arrowPool.RestorePool(gameObject);
            enemy.TakeDamage(_arrowPool.ArrowDamage);
        }
    }

    public void Initialize(ArrowPool arrowPool)
    {
        _arrowPool = arrowPool;
    }
}
