using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public static Action<GameObject> OnEnemyDestroy;
    public static Action<GameObject> OnEnemyDisappear;
    public static Action<float> OnEnemyDamage;

    [SerializeField] private EnemyData data;

    private Transform playerObject;
    private SpriteRenderer enemyRender;
    private float _currentHealth;
    public int ExpEarned => data.expEarned;
    public float Damage => data.damage;
    private float maxDistance = 20f;
    private Rigidbody2D rb;

    void Awake()
    {
        playerObject = FindAnyObjectByType<PlayerMovement>().transform;
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
            playerObject.position,
            data.speed * Time.fixedDeltaTime
        );

        rb.MovePosition(nextPosition);

        float deltaX = playerObject.position.x - rb.position.x;
        if (Mathf.Abs(deltaX) > 0.01f)
            enemyRender.flipX = deltaX < 0f;

        if(Vector3.Distance(playerObject.transform.position, transform.position) >= maxDistance)
        {
            OnEnemyDisappear?.Invoke(gameObject);
        }
    }
    
    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);

        if(_currentHealth == 0)
        {
            OnEnemyDestroy?.Invoke(gameObject);
        }
    }
}
