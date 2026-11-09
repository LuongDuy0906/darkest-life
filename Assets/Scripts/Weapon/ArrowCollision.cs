using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private ArrowPool _arrowPool;
    private AudioManager _audioManager;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            _audioManager.PlayHitEnemySound();
            _arrowPool.RestorePool(gameObject);
            enemy.TakeDamage(_arrowPool.ArrowDamage);
        }
    }

    public void Initialize(ArrowPool arrowPool, AudioManager audioManager)
    {
        _arrowPool = arrowPool;
        _audioManager = audioManager;
    }
}
