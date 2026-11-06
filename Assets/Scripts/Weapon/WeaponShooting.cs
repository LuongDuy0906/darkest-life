using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponShooting : MonoBehaviour
{
    private ArrowPool _arrowPool;
    private GameManager gameManager;
    private readonly List<RaycastResult> _raycastResults = new();

    private void Start() {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && gameManager.GetGameState() == GameState.Playing && !IsPointerOverUI())
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        if(gameManager.GetGameState() != GameState.Playing) return;
        _arrowPool.GetPool(transform.position, transform.rotation);
    }

    private bool IsPointerOverUI()
    {
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem == null) return false;

        var pointerData = new PointerEventData(eventSystem)
        {
            position = Mouse.current.position.ReadValue()
        };

        _raycastResults.Clear();
        eventSystem.RaycastAll(pointerData, _raycastResults);

        foreach (RaycastResult hit in _raycastResults)
        {
            if (hit.module is GraphicRaycaster)
                return true;
        }

        return false;
    }
}
