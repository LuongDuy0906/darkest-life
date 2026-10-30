using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _timePlayText;

    void Awake()
    {
        gameOverUI.SetActive(false);
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void UpdateTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        _timePlayText.text = $"{minutes}:{seconds}";
    }

    public void UpdateGamePlayUI(int totalScore, float timePlay)
    {
        int bestScore = PlayerPrefs.GetInt("bestScore");

        gameOverUI.SetActive(true);
        _bestScoreText.text = bestScore.ToString();
        _currentScoreText.text = totalScore.ToString();
        UpdateTimeText(timePlay);
        _scoreText.gameObject.SetActive(false);
    }
}
