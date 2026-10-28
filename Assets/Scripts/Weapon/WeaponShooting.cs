using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private GameObject arrowsPrefab;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        Instantiate(arrowsPrefab, transform.position, transform.rotation);
    }
}
