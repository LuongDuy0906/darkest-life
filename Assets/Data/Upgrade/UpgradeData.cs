using UnityEngine;

public enum UpgradeType
{
    ArrowSpeed,
    Damage,
    Health
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string upgradeInfo;
    public Sprite icon;
    public UpgradeType upgradeType;
    public float upgradeValue;
}
