using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private float speed = 5f;
    private float _maxDistance = 12f;
    private ArrowPool _arrowPool;
    private Vector3 _startPosition;

    void OnEnable()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

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
