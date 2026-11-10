using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI arrowsText;

    private int lastDisplayedSecond = -1;

    private Vector3 _expBarOriginalScale;

    public void GetExpOriginalScale()
    {
        _expBarOriginalScale = expBar.localScale;
        expBar.localScale = new Vector3(0f, _expBarOriginalScale.y, _expBarOriginalScale.z);
    }

    public void ShowUI()
    {
        levelText.gameObject.SetActive(true);
        expBar.gameObject.SetActive(true);
        pauseButton.SetActive(true);
        gameOverUI.SetActive(false);
        upgradePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    private string StandardizeDisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
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

        levelText.gameObject.SetActive(false);
        expBar.gameObject.SetActive(false);
        pauseButton.SetActive(false);
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
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void ShowPausePanel(bool isGamePause)
    {
        pausePanel.SetActive(isGamePause);
    }

    public void ChangeArrowText(int arrows)
    {
        arrowsText.text = arrows.ToString();
    }
}
