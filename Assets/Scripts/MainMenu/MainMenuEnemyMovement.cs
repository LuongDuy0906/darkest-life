using UnityEngine;

public class MainMenuEnemyMovement : MonoBehaviour
{
    private Vector2 _centerPosition;
    private float _radius = 2f;
    private float _waitTimer = 0;
    private float _speed = 2f;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _targetLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _centerPosition = gameObject.transform.position;
        _targetLocation = RandomLocation();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    private Vector2 RandomLocation()
    {
        return _centerPosition + (Random.insideUnitCircle * _radius);
    }

    private void EnemyMove()
    {
        if (_waitTimer > 0f)
        {
            _waitTimer -= Time.deltaTime;
            return;
        }

        FlipEnemy(_targetLocation);

        Vector2 next = Vector2.MoveTowards(
            transform.position,
            _targetLocation,
            _speed * Time.deltaTime
        );

        transform.position = new Vector3(next.x, next.y, transform.position.z);

        if (Vector2.Distance(next, _targetLocation) < 0.05f)
        {
            _waitTimer = Random.Range(0.3f, 1f);
            _targetLocation = RandomLocation();
        }
    }

    private void FlipEnemy(Vector2 location)
    {
        float deltaX = location.x - transform.position.x;

        if (Mathf.Abs(deltaX) > 0.01f)
        {
            _spriteRenderer.flipX = deltaX < 0f;
        }
    }
}
