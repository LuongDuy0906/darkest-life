using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnScoreChanged;
    public static Action<int, float> OnGameOver;

    private int _totalScore = 0;
    private bool _isGameOver = false;
    private float _timeSurvive = 0f;

    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
        PlayerHeatlh.OnPlayerDie += HandlePlayerDie;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
        PlayerHeatlh.OnPlayerDie -= HandlePlayerDie;
    }

    void Update()
    {
        _timeSurvive += Time.deltaTime;
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {
        Enemy enemyDestroyed = enemy.GetComponent<Enemy>();

        _totalScore += enemyDestroyed.ScoreEarned;

        OnScoreChanged?.Invoke(_totalScore);
    }

    private void StorePlayerStat()
    {
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);

        if(_totalScore >= bestScore)
        {
            PlayerPrefs.SetInt("bestScore", _totalScore);
        }

        PlayerPrefs.SetFloat("timeSurvice", _timeSurvive);
        PlayerPrefs.Save();
    }

    private void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    private void HandlePlayerDie()
    {
        _isGameOver = true;
        SetTimeScale(0);
        StorePlayerStat();
        OnGameOver?.Invoke(_totalScore, _timeSurvive);
    }   
}
