using System;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeSurvivedText;
    [SerializeField] private TextMeshProUGUI bestLevelText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI timePlayText;
    [SerializeField] private Transform expBar;
    [SerializeField] private GameObject upgradePanel;

    private int lastDisplayedSecond = -1;

    private Vector3 _expBarOriginalScale;

    void Awake()
    {
        gameOverUI.SetActive(false);
        _expBarOriginalScale = expBar.localScale;
        expBar.localScale = new Vector3(0f, _expBarOriginalScale.y, _expBarOriginalScale.z);
        upgradePanel.SetActive(false);
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    private string StandardizeDisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes}:{seconds}";
    }

    public void UpdateTimePlayText(float timePlay)
    {
        timeSurvivedText.text = StandardizeDisplayTime(timePlay);
    }

    private void UpdateTimeText(float time)
    {
        timePlayText.text = StandardizeDisplayTime(time);
    }

    public void UpdateGamePlayUI(int totalScore, float timePlay)
    {
        int bestScore = PlayerPrefs.GetInt("bestLevel");

        gameOverUI.SetActive(true);
        bestLevelText.text = bestScore.ToString();
        currentLevelText.text = totalScore.ToString();
        UpdateTimeText(timePlay);
        levelText.gameObject.SetActive(false);
    }

    public void UpdateExpBar(float expPercent)
    {
        Vector3 scale = _expBarOriginalScale;
        scale.x = _expBarOriginalScale.x * expPercent;
        expBar.localScale = scale;
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);

        UpgradePanel panel = upgradePanel.GetComponent<UpgradePanel>();

        panel.ShowUpgradeCard();
    }
}
