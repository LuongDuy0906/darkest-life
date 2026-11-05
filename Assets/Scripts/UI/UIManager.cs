using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GamePlayUI gamePlayUI;
    private GameManager gameManager;
    private int lastDisplayedSecond = -1;

    void Awake()
    {
        gamePlayUI = FindAnyObjectByType<GamePlayUI>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnEnable()
    {
        GameManager.OnLevelChanged += HandleLevelChanged;
        GameManager.OnGameOver += HandleGameOver;
        PlayerExp.OnExpChanged += HandleExpChanged;
        PlayerExp.OnLevelUp += HandleLevelUp;
    }

    void OnDisable()
    {
        GameManager.OnLevelChanged -= HandleLevelChanged;
        GameManager.OnGameOver -= HandleGameOver;
        PlayerExp.OnExpChanged -= HandleExpChanged;
        PlayerExp.OnLevelUp -= HandleLevelUp;
    }

    void Update()
    {
        HandleUpdateTimePlay();
    }

    private void HandleGameOver(int totalScore, float timePlay)
    {
        gamePlayUI.UpdateGamePlayUI(totalScore, timePlay);
    }

    private void HandleExpChanged(float expPercent)
    {
        gamePlayUI.UpdateExpBar(expPercent);
    }

    private void HandleLevelUp(int level)
    {
        gamePlayUI.ShowUpgradePanel();
    }

    private void HandleLevelChanged(int level)
    {
        gamePlayUI.UpdateLevelText(level);
    }

    private void HandleUpdateTimePlay()
    {
        int second = Mathf.FloorToInt(gameManager.TimeSurvived);
        if (second == lastDisplayedSecond) return;

        lastDisplayedSecond = second;
        gamePlayUI.UpdateTimePlayText(second);
    }
}
