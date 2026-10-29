using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public static Action<GameObject> OnEnemyDestroy;
    public static Action<float> OnEnemyDamage;

    [SerializeField] private EnemyData data;

    private Transform playerObject;
    private SpriteRenderer enemyRender;
    private float speed = 1.5f;
    private float _currentHealth;

    void Awake()
    {
        playerObject = FindAnyObjectByType<PlayerMovement>().transform;
        enemyRender = GetComponent<SpriteRenderer>();   
        _currentHealth = data.maxHealth;     
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        float deltaX = playerObject.position.x - transform.position.x;

        if (Mathf.Abs(deltaX) > 0.01f)
            enemyRender.flipX = deltaX < 0f;

        transform.position = Vector2.MoveTowards(transform.position, playerObject.position, speed * Time.deltaTime);
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);

        if(_currentHealth == 0)
        {
            OnEnemyDestroy?.Invoke(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnEnemyDamage?.Invoke(data.damage);
        }
    }
}
