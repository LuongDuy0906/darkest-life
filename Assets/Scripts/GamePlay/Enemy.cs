using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private Transform playerObject;
    private SpriteRenderer enemyRender;
    private float speed = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = FindAnyObjectByType<PlayerMovement>().transform;
        enemyRender = GetComponent<SpriteRenderer>();
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
}
