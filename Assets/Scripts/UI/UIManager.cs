using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GamePlayUI gamePlayUI;

    void Awake()
    {
        gamePlayUI = FindAnyObjectByType<GamePlayUI>();
    }

    void OnEnable()
    {
        GameManager.OnScoreChanged += HandleScoreChanged;
        GameManager.OnGameOver += HandleGameOver;
        PlayerExp.OnExpChanged += HandleExpChanged;
    }

    void OnDisable()
    {
        GameManager.OnScoreChanged -= HandleScoreChanged;
        GameManager.OnGameOver -= HandleGameOver;
        PlayerExp.OnExpChanged -= HandleExpChanged;
    }

    private void UpdateGamePlayUI(int score)
    {
        gamePlayUI.UpdateScoreText(score);
    }

    private void HandleScoreChanged(int score)
    {
        UpdateGamePlayUI(score);
    }

    private void HandleGameOver(int totalScore, float timePlay)
    {
        gamePlayUI.UpdateGamePlayUI(totalScore, timePlay);
    }

    private void HandleExpChanged(float expPercent)
    {
        gamePlayUI.UpdateExpBar(expPercent);
    }
}
