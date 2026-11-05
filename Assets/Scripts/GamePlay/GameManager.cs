using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int, float> OnGameOver;

    private bool _isGameOver = false;
    private int _currentLevel;
    private float _timeSurvive = 0f;

    public float TimeSurvived => _timeSurvive;

    void OnEnable()
    {
        PlayerHeatlh.OnPlayerDie += HandlePlayerDie;
        PlayerExp.OnLevelUp += HandleLevelUp;
    }

    void OnDisable()
    {
        PlayerHeatlh.OnPlayerDie -= HandlePlayerDie;
        PlayerExp.OnLevelUp -= HandleLevelUp;
    }

    void Update()
    {   
        if(_isGameOver) return;
        _timeSurvive += Time.deltaTime;
    }

    private void StorePlayerStat()
    {
        int bestLevel = PlayerPrefs.GetInt("bestLevel", 0);

        if(_currentLevel >= bestLevel)
        {
            PlayerPrefs.SetInt("bestLevel", _currentLevel);
        }

        PlayerPrefs.SetFloat("timeSurvive", _timeSurvive);
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
        OnGameOver?.Invoke(_currentLevel, _timeSurvive);
    }   

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    private void HandleLevelUp(int level)
    {
        SetTimeScale(0);
        _currentLevel = level;
    }

    public void ResumeGame()
    {
        SetTimeScale(1);
    }
}
