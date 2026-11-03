using System;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public static event Action<float> OnExpChanged;

    private float _currentExp;
    private float _maxExp;
    private int _currentLevel;

    void Awake()
    {
        _currentExp = 0f;
        _currentLevel = 1;
        _maxExp = RequiredExp(_currentLevel);
    }

    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
    }

    private int RequiredExp(int level)
    {
        return 10 + (level - 1) * 5;
    }

    private void EarnedExp(int expEarned)
    {
        _currentExp += expEarned;
        float expPercent = _currentExp / _maxExp;

        OnExpChanged?.Invoke(expPercent);

        if(_currentExp >= _maxExp)
        {
            _currentExp -= _maxExp;
            _currentLevel += 1;
            _maxExp = RequiredExp(_currentLevel);
        }
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {
        Enemy enemyDestroyed = enemy.GetComponent<Enemy>();

        EarnedExp(enemyDestroyed.ExpEarned);
    }
}
