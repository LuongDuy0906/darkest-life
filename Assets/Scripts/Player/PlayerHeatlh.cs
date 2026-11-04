using System;
using UnityEngine;

public class PlayerHeatlh : MonoBehaviour
{
    public static Action OnPlayerDie;

    [SerializeField] private Transform healthBar;

    private float _currentLives;
    private float _maxLives = 100;
    private Vector3 _healthBarOriginalScale;
    private GameManager gameManager;
    private bool _isTakeDamage = false;
    private Animator animator;

    void Awake()
    {
        _healthBarOriginalScale = healthBar.localScale;
        _currentLives = _maxLives;
        gameManager = FindAnyObjectByType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        PlayerCollision.OnTakeDamage += HandleTakeDamage;
    }

    void OnDisable()
    {
        PlayerCollision.OnTakeDamage -= HandleTakeDamage;
    }

    private void UpdateHealthBar()
    {
        float healthPercent = _currentLives / _maxLives;
        Vector3 scale = _healthBarOriginalScale;
        scale.x = _healthBarOriginalScale.x * healthPercent;
        healthBar.localScale = scale;
    }

    private void HandleTakeDamage(float damage)
    {
        if(gameManager.IsGameOver()) return;

        _currentLives -= damage;
        _currentLives = Mathf.Max(_currentLives, 0);
        UpdateHealthBar();

        if(_currentLives == 0)
        {
            OnPlayerDie?.Invoke();
        }
    }

    public void SetIsTakeDamage(bool isTakeDamage)
    {
        _isTakeDamage = isTakeDamage;
        animator.SetBool("isHurt", isTakeDamage);
    }
}
