using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = FindAnyObjectByType<TextMeshProUGUI>();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
