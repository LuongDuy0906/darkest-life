using UnityEngine;

public class UpgradeManager : MonoBehaviour
{   
    private ArrowPool _arrowPool;
    private PlayerHeatlh _playerHealth;

    void Awake()
    {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
        _playerHealth = FindAnyObjectByType<PlayerHeatlh>();
    }

    public void HandleUpgrade(UpgradeData data)
    {
        switch(data.upgradeType)
        {
            case UpgradeType.ArrowSpeed:
                _arrowPool.UpdateArrowSpeed(data.upgradeValue);
                break;
            case UpgradeType.Damage:
                _arrowPool.UpgradeArrowDamage(data.upgradeValue);
                break;
            case UpgradeType.Health:
                _playerHealth.UpgradePlayerHealth(data.upgradeValue);
                break;
        }
    }
}
