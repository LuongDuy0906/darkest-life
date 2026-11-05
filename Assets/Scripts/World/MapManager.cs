using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private int chunkCellsX = 20;
    [SerializeField] private int chunkCellsY = 20;

    private readonly GameObject[] chunks = new GameObject[9];
    private Transform playerPosition;
    private float chunkWidth;
    private float chunkHeight;
    private Vector2Int lastCenterChunk;

    private void Start()
    {
        Grid grid = chunkPrefab.GetComponent<Grid>();
        playerPosition = FindAnyObjectByType<PlayerMovement>().transform;

        chunkWidth = chunkCellsX * grid.cellSize.x;
        chunkHeight = chunkCellsY * grid.cellSize.y;

        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i] = Instantiate(chunkPrefab, transform);
        }

        lastCenterChunk = GetPlayerChunkCoord();
        PositionChunks(lastCenterChunk);
    }

    private void Update()
    {
        Vector2Int centerChunk = GetPlayerChunkCoord();

        if (centerChunk == lastCenterChunk)
            return;

        lastCenterChunk = centerChunk;
        PositionChunks(centerChunk);
    }

    private Vector2Int GetPlayerChunkCoord()
    {
        int x = Mathf.FloorToInt(playerPosition.position.x / chunkWidth);
        int y = Mathf.FloorToInt(playerPosition.position.y / chunkHeight);

        return new Vector2Int(x, y);
    }

    private void PositionChunks(Vector2Int centerChunk)
    {
        int index = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                chunks[index].transform.position = new Vector3(
                    (centerChunk.x + x) * chunkWidth,
                    (centerChunk.y + y) * chunkHeight,
                    0f
                );

                index++;
            }
        }
    }
}