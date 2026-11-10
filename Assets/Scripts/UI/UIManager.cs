using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private MainMenuUI mainMenuUI;
    private int lastDisplayedSecond = -1;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gamePlayUI.GetExpOriginalScale();
    }

    void OnEnable()
    {
        GameManager.OnGameOver += HandleGameOver;
        PlayerExp.OnExpChanged += HandleExpChanged;
        PlayerExp.OnLevelUp += HandleLevelUp;
        GameManager.OnStillLevelingUp += HandleStillLevelingUp;
        ArrowPool.OnArrowsChanged += HandleArrowsChanged;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
        PlayerExp.OnExpChanged -= HandleExpChanged;
        PlayerExp.OnLevelUp -= HandleLevelUp;
        GameManager.OnStillLevelingUp -= HandleStillLevelingUp;
        ArrowPool.OnArrowsChanged -= HandleArrowsChanged;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void Update()
    {
        if(GameManager.Instance.GetGameState() == GameState.Playing)
        {
            HandleUpdateTimePlay();
        }

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
        gamePlayUI.UpdateLevelText(level);

        if(level <= 1) return;

        if(GameManager.Instance.GetGameState() == GameState.GameOver || GameManager.Instance.GetGameState() == GameState.Paused) return;
        gamePlayUI.ShowUpgradePanel();
    }

    private void HandleUpdateTimePlay()
    {
        int second = Mathf.FloorToInt(GameManager.Instance.TimeSurvived);
        if (second == lastDisplayedSecond) return;

        lastDisplayedSecond = second;
        gamePlayUI.UpdateTimePlayText(second);
    }

    public void PauseGame()
    {
        GameManager.Instance.TogglePauseMenu();
        gamePlayUI.ShowPausePanel(GameManager.Instance.GetGameState() == GameState.Paused);
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

    private void HandleArrowsChanged(int arrows)
    {
        gamePlayUI.ChangeArrowText(arrows);
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameManager.Instance.GetGameState() == GameState.MainMenu)
        {
            gamePlayUI.gameObject.SetActive(false);
            mainMenuUI.gameObject.SetActive(true);
        } else
        {
            gamePlayUI.gameObject.SetActive(true);
            gamePlayUI.ShowUI();
            mainMenuUI.gameObject.SetActive(false);
        }
    }
}
