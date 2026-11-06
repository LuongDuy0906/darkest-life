using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private InputActionReference moveInput;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 _move;
    private SpriteRenderer playerSpriteRender;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        moveInput.action.Enable();
    }

    private void OnDisable() {
        moveInput.action.Disable();
    }

    private void Update() {
        _move = moveInput.action.ReadValue<Vector2>();
        UpdateAnimation();
        Flip();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.linearVelocity = _move * speed;
    }

    private void Flip()
    {
        if(_move.x != 0)
        {
            playerSpriteRender.flipX = _move.x < 0;
        }
    }

    private void UpdateAnimation()
    {
        bool isWalk = Mathf.Abs(rb.linearVelocity.x) > 0.1f || Mathf.Abs(rb.linearVelocity.y) > 0.1f;

        animator.SetBool("isWalk", isWalk);
    }
}
