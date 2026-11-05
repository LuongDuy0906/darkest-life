using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private float _maxDistance = 12f;
    private ArrowPool _arrowPool;
    private Vector3 _startPosition;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _startPosition = transform.position;
        rb.linearVelocity = transform.right * _arrowPool.ArrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
        {
            _arrowPool.RestorePool(gameObject);
        }
    }

    public void Initialize(ArrowPool arrowPool)
    {
        _arrowPool = arrowPool;
    }
}
