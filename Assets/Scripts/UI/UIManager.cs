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
    }

    private void UpdateGamePlayUI(int score)
    {
        gamePlayUI.UpdateScoreText(score);
    }

    private void HandleScoreChanged(int score)
    {
        UpdateGamePlayUI(score);
    }

    private void HandleGameOver()
    {
        gamePlayUI.UpdateGamePlayUI();
    }
}
