using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradeData[] upgrades;
    [SerializeField] private GameObject upgradeCardPrefab;
    [SerializeField] private Transform cardsContainer;

    private List<GameObject> activeCards = new List<GameObject>();

    private UpgradeManager _upgradeManager;
    private GameManager _gameManager;

    void Awake()
    {
        _upgradeManager = FindAnyObjectByType<UpgradeManager>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnEnable()
    {
        UpgradeCard.OnCardClicked += HandleCardClick;
        ShowUpgradeCard();
    }

    void OnDisable()
    {
        UpgradeCard.OnCardClicked -= HandleCardClick;
    }

    public void ShowUpgradeCard()
    {
        foreach(var card in activeCards)
        {
            Destroy(card);
        }

        activeCards.Clear();

        foreach(var data in upgrades)
        {
            GameObject cardGameObject = Instantiate(upgradeCardPrefab, cardsContainer);
            UpgradeCard card = cardGameObject.GetComponent<UpgradeCard>();
            card.Initialize(data);
            activeCards.Add(cardGameObject);
        }
    }

    private void HandleCardClick(UpgradeData data)
    {
        _upgradeManager.HandleUpgrade(data);
        gameObject.SetActive(false);
        _gameManager.ResumeGame();
    }
}
