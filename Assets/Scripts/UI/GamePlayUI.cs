using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private TextMeshProUGUI _scoreText;

    void Awake()
    {
        _scoreText = FindAnyObjectByType<TextMeshProUGUI>();
        gameOverUI.SetActive(false);
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateGamePlayUI()
    {
        gameOverUI.SetActive(true);
        _scoreText.gameObject.SetActive(false);
    }
}
