using System;
using UnityEngine;

public class PlayerHeatlh : MonoBehaviour
{
    public static Action OnPlayerDie;

    [SerializeField] private Transform healthBar;
    [SerializeField] private float damageCooldown = 0.5f;

    private float _currentLives;
    private float _maxLives = 30;
    private Vector3 _healthBarOriginalScale;
    private GameManager gameManager;
    private Animator animator;
    private float nextDamageTime;

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
        if(gameManager.GetGameState() != GameState.Playing) return;
        if (Time.time < nextDamageTime) return;

        nextDamageTime = Time.time + damageCooldown;

        _currentLives -= damage;
        _currentLives = Mathf.Max(_currentLives, 0);
        animator.SetTrigger("Hurt");
        UpdateHealthBar();

        if(_currentLives == 0)
        {
            OnPlayerDie?.Invoke();
        }
    }

    public void UpgradePlayerHealth(float health)
    {
        if (_currentLives >= _maxLives)
        {
            _maxLives += health;
            _currentLives = _maxLives;
        }
        else
        {
            _currentLives = Mathf.Min(_currentLives + health, _maxLives);
        }

        UpdateHealthBar();
    }
}
