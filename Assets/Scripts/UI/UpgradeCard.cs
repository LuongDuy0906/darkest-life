using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Image upgradeCardImage;
    [SerializeField] private TMP_Text upgradeName;
    [SerializeField] private TMP_Text upgradeInfo;

    public void Initialize(UpgradeData data)
    {
        upgradeCardImage.sprite = data.icon;
        upgradeName.text = data.upgradeName;
        upgradeInfo.text = data.upgradeInfo;
    }
}
