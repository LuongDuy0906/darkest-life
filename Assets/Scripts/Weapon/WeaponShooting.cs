using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponShooting : MonoBehaviour
{
    private ArrowPool _arrowPool;

    private void Start() {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        _arrowPool.GetPool(transform.position, transform.rotation);
    }
}
