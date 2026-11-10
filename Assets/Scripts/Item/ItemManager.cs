using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemPool> itemPools;

    private sealed class DroppedItem
    {
        public readonly ItemPool Pool;
        public readonly Vector3 WorldPosition;
        public readonly Vector2Int ChunkCoord;
        public GameObject Visual;

        public DroppedItem(ItemPool pool, Vector3 worldPosition, Vector2Int chunkCoord)
        {
            Pool = pool;
            WorldPosition = worldPosition;
            ChunkCoord = chunkCoord;
        }
    }

    private readonly Dictionary<Vector2Int, List<DroppedItem>> _dropsByChunk = new();
    private readonly Dictionary<GameObject, DroppedItem> _dropsByVisual = new();
    private readonly HashSet<Vector2Int> _visibleChunkCoords = new();

    private ArrowPool _arrowPool;
    private PlayerExp _playerExp;
    private MapManager _mapManager;

    void Awake()
    {
        _arrowPool = FindAnyObjectByType<ArrowPool>();
        _playerExp = FindAnyObjectByType<PlayerExp>();
        _mapManager = FindAnyObjectByType<MapManager>();
    }

    void OnEnable()
    {
        Enemy.OnEnemyDestroy += HandleEnemyDestroy;
        PlayerCollision.OnTakeItem += HandleItemTaken;
        _mapManager.OnCenterChunkChanged += HandleCenterChunkChanged;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDestroy -= HandleEnemyDestroy;
        PlayerCollision.OnTakeItem -= HandleItemTaken;
        _mapManager.OnCenterChunkChanged -= HandleCenterChunkChanged;
    }

    private void HandleEnemyDestroy(GameObject enemy)
    {
        Enemy destroyedEnemy = enemy.GetComponent<Enemy>();

        foreach (ItemPool itemPool in itemPools)
        {
            if (!destroyedEnemy.GetItemDrop().Contains(itemPool.ItemData)) continue;

            int count = destroyedEnemy.GetNumberItemToReturn(itemPool.ItemData.itemType);
            for (int i = 0; i < count; i++)
            {
                Vector3 worldPosition = itemPool.RandomSpawnPosition(enemy.transform.position);
                Vector2Int chunkCoord = _mapManager.GetChunkCoord(worldPosition);
                DroppedItem drop = new(itemPool, worldPosition, chunkCoord);

                if (!_dropsByChunk.TryGetValue(chunkCoord, out List<DroppedItem> drops))
                {
                    drops = new List<DroppedItem>();
                    _dropsByChunk.Add(chunkCoord, drops);
                }

                drops.Add(drop);

                if (_visibleChunkCoords.Contains(chunkCoord))
                    ShowDrop(drop);
            }
        }
    }

    private void HandleItemTaken(Item item)
    {
        if (!_dropsByVisual.TryGetValue(item.gameObject, out DroppedItem drop)) return;

        bool itemCollected = false;
        switch (item.ItemType)
        {
            case ItemType.Arrow:
                itemCollected = _arrowPool.AddArrow();
                break;
            case ItemType.Exp:
                _playerExp.EarnedExp(1);
                itemCollected = true;
                break;
        }

        if (!itemCollected) return;

        HideDrop(drop);
        List<DroppedItem> drops = _dropsByChunk[drop.ChunkCoord];
        drops.Remove(drop);
        if (drops.Count == 0)
            _dropsByChunk.Remove(drop.ChunkCoord);
    }

    private void HandleCenterChunkChanged(Vector2Int centerChunk)
    {
        HashSet<Vector2Int> nextVisible = new();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
                nextVisible.Add(centerChunk + new Vector2Int(x, y));
        }

        foreach (Vector2Int coord in _visibleChunkCoords)
        {
            if (nextVisible.Contains(coord) || !_dropsByChunk.TryGetValue(coord, out List<DroppedItem> drops))
                continue;

            foreach (DroppedItem drop in drops)
                HideDrop(drop);
        }

        foreach (Vector2Int coord in nextVisible)
        {
            if (_visibleChunkCoords.Contains(coord) || !_dropsByChunk.TryGetValue(coord, out List<DroppedItem> drops))
                continue;

            foreach (DroppedItem drop in drops)
                ShowDrop(drop);
        }

        _visibleChunkCoords.Clear();
        _visibleChunkCoords.UnionWith(nextVisible);
    }

    private void ShowDrop(DroppedItem drop)
    {
        if (drop.Visual != null) return;

        GameObject visual = drop.Pool.GetPool(drop.WorldPosition);
        drop.Visual = visual;
        _dropsByVisual.Add(visual, drop);
    }

    private void HideDrop(DroppedItem drop)
    {
        if (drop.Visual == null) return;

        _dropsByVisual.Remove(drop.Visual);
        drop.Pool.RestorePool(drop.Visual);
        drop.Visual = null;
    }
}
