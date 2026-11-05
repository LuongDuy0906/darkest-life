using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public event Action<UpgradeData> OnCardClicked;

    [SerializeField] private Image upgradeCardImage;
    [SerializeField] private TMP_Text upgradeName;
    [SerializeField] private TMP_Text upgradeInfo;

    private UpgradeData _upgrade;

    public void Initialize(UpgradeData data)
    {
        _upgrade = data;
        upgradeCardImage.sprite = data.icon;
        upgradeName.text = data.upgradeName;
        upgradeInfo.text = data.upgradeInfo;
    }

    public void ChooseUpgrade()
    {
        OnCardClicked?.Invoke(_upgrade);
    }
}
