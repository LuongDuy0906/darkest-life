using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnScoreChanged;

    private int _totalScore = 0;

    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {
        Enemy enemyDestroyed = enemy.GetComponent<Enemy>();

        _totalScore += enemyDestroyed.ScoreEarned;

        OnScoreChanged?.Invoke(_totalScore);
    }
}
