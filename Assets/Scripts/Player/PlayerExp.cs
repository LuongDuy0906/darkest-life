using System;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public static event Action<float> OnExpChanged;
    public static event Action<int> OnLevelUp;

    private float _currentExp;
    private float _maxExp;
    private int _currentLevel;
    private AudioManager _audioManager;

    void Awake()
    {
        _currentExp = 0f;
        _currentLevel = 1;
        _maxExp = RequiredExp(_currentLevel);
        _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private int RequiredExp(int level)
    {
        return 10 + (level - 1) * 5;
    }

    public void EarnedExp(int expEarned)
    {
        _audioManager.PlayExpOrbCollectedSound();
        _currentExp += expEarned;

        while(_currentExp >= _maxExp)
        {
            _currentExp -= _maxExp;
            _currentLevel += 1;
            _maxExp = RequiredExp(_currentLevel);
            OnLevelUp?.Invoke(_currentLevel);
        }

        float expPercent = _currentExp / _maxExp;

        OnExpChanged?.Invoke(expPercent);
    }
}
