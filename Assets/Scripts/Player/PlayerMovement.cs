
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private InputActionReference moveInput;

    private Rigidbody2D rb;
    private Animator animator;
    private float _moveX;
    private float _moveY;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        moveInput.action.Enable();
    }

    private void Update() {
        Movement();
        UpdateAnimation();
    }

    private void Movement()
    {
        float _moveX = moveInput.action.ReadValue<Vector2>().x;
        float _moveY = moveInput.action.ReadValue<Vector2>().y;
 
        rb.linearVelocity = new Vector2(speed * _moveX, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed * _moveY);

        if (_moveX != 0)
            transform.localScale = new Vector3(Mathf.Sign(_moveX), 1, 1);
    }

    private void UpdateAnimation()
    {
        bool isWalk = Mathf.Abs(rb.linearVelocity.x) > 0.1f;

        animator.SetBool("isWalk", isWalk);
    }
}
