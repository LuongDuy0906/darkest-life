using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
        GameManager.OnGameOver += HandleGameOver;
        PlayerExp.OnExpChanged += HandleExpChanged;
        PlayerExp.OnLevelUp += HandleLevelUp;
        GameManager.OnStillLevelingUp += HandleStillLevelingUp;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
        PlayerExp.OnExpChanged -= HandleExpChanged;
        PlayerExp.OnLevelUp -= HandleLevelUp;
        GameManager.OnStillLevelingUp -= HandleStillLevelingUp;
    }

    void Update()
    {
        HandleUpdateTimePlay();

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }
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
        if(gameManager.GetGameState() == GameState.GameOver || gameManager.GetGameState() == GameState.Paused) return;
        gamePlayUI.UpdateLevelText(level);
        gamePlayUI.ShowUpgradePanel();
    }

    private void HandleUpdateTimePlay()
    {
        int second = Mathf.FloorToInt(gameManager.TimeSurvived);
        if (second == lastDisplayedSecond) return;

        lastDisplayedSecond = second;
        gamePlayUI.UpdateTimePlayText(second);
    }

    public void PauseGame()
    {
        gameManager.TogglePauseMenu();
        gamePlayUI.ShowPausePanel(gameManager.GetGameState() == GameState.Paused);
    }

    public IEnumerator ShowNextUpgradePanel()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        gamePlayUI.ShowUpgradePanel();
    }

    private void HandleStillLevelingUp()
    {
        StartCoroutine(ShowNextUpgradePanel());
    }
}
