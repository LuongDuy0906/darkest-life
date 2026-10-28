using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());

        Vector2 direction = mousePoint - (Vector2) transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
