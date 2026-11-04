using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponShooting : MonoBehaviour
{
    private ArrowPool _arrowPool;
    private GameManager gameManager;

    private void Start() {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && !gameManager.IsGameOver())
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        _arrowPool.GetPool(transform.position, transform.rotation);
    }
}
