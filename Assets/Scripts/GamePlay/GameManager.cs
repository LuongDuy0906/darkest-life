using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    LevelUp,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static Action<int, float> OnGameOver;
    public static Action OnStillLevelingUp;

    private int _currentLevel = 1;
    private float _timeSurvive = 0f;
    public float TimeSurvived => _timeSurvive;
    public float CurrentLevel => _currentLevel;
    private GameState _currentState;
    private int _pendingUpdateChoice;

    void Awake()
    {
       SetGameState(GameState.Playing);
       _pendingUpdateChoice = 0;
    }

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
        if(_currentState != GameState.Playing) return;
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
        SetGameState(GameState.GameOver);
        StorePlayerStat();
        OnGameOver?.Invoke(_currentLevel, _timeSurvive);
    }   

    private void HandleLevelUp(int level)
    {   
        if(_currentState == GameState.GameOver || _currentState == GameState.Paused) return;
        SetGameState(GameState.LevelUp);
        _currentLevel = level;
        _pendingUpdateChoice += 1;
    }

    public void FinishedLevelUp()
    {
        if(_currentState == GameState.LevelUp)
        {
            if (_pendingUpdateChoice <= 0) return;

            _pendingUpdateChoice--;

            if (_pendingUpdateChoice > 0)
            {
                OnStillLevelingUp?.Invoke();
                return;
            }

            SetGameState(GameState.Playing);
        } else
        {
            return;
        }
    }

    public void TogglePauseMenu()
    {   
        if(_currentState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        } else if(_currentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
    }

    public void SetGameState(GameState state)
    {
        _currentState = state;
        SetTimeScale(state == GameState.Playing ? 1 : 0);
    }

    public GameState GetGameState()
    {
        return _currentState;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
