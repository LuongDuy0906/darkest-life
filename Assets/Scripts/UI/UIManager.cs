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
    }

    private void UpdateGamePlayUI(int score)
    {
        gamePlayUI.UpdateScoreText(score);
    }

    private void HandleScoreChanged(int score)
    {
        UpdateGamePlayUI(score);
    }
}
