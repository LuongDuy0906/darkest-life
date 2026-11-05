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
        if(Mouse.current.leftButton.wasPressedThisFrame && gameManager.GetGameState() == GameState.Playing)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        if(gameManager.GetGameState() != GameState.Playing) return;
        _arrowPool.GetPool(transform.position, transform.rotation);
    }
}
