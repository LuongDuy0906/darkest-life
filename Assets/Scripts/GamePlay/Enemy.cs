using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public static Action<GameObject> OnEnemyDestroy;
    public static Action<GameObject> OnEnemyDisappear;
    public static Action<float> OnEnemyDamage;

    [SerializeField] private EnemyData data;

    private Transform _playerTransform;
    private SpriteRenderer enemyRender;
    private float _currentHealth;
    public int ExpEarned => data.expEarned;
    public float Damage => data.damage;
    private float maxDistance = 20f;
    private Rigidbody2D rb;

    void Awake()
    {
        enemyRender = GetComponent<SpriteRenderer>();     
        rb = GetComponent<Rigidbody2D>();  
    }

    void OnEnable()
    {
        _currentHealth = data.maxHealth; 
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        Vector2 nextPosition = Vector2.MoveTowards(
            rb.position,
            _playerTransform.position,
            data.speed * Time.fixedDeltaTime
        );

        rb.MovePosition(nextPosition);

        float deltaX = _playerTransform.position.x - rb.position.x;
        if (Mathf.Abs(deltaX) > 0.01f)
            enemyRender.flipX = deltaX < 0f;

        if(Vector3.Distance(_playerTransform.transform.position, transform.position) >= maxDistance)
        {
            OnEnemyDisappear?.Invoke(gameObject);
        }
    }
    
    public void TakeDamage(float damage)
    {
        if(_currentHealth <= 0f)
        {
            return;
        }

        _currentHealth = Mathf.Max(0f, _currentHealth - damage);

        if(_currentHealth == 0f)
        {
            OnEnemyDestroy?.Invoke(gameObject);
        }
    }

    public void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public List<ItemData> GetItemDrop()
    {
        return data.dropItems;
    }

    public int GetNumberItemToReturn(ItemType type)
    {
        switch(type)
        {
            case ItemType.Exp:
                return data.expEarned;
            case ItemType.Arrow:
                return data.arrowEarned;
            default:
                return 0;
        }
    }
}
