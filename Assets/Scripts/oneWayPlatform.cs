using UnityEngine;
using UnityEngine.Tilemaps;

public class oneWayPlatform : MonoBehaviour
{
    public Tilemap sourceTilemap;      // visual tiles (no collider)
    public Tilemap colliderTilemap;    // tilemap that has TilemapCollider2D (used for collisions)
    public Transform player;
    public int verticalBufferTiles = 0; // optional: number of tiles above platform considered "above"

    void Start()
    {
        if (sourceTilemap == null)
            sourceTilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (sourceTilemap == null || colliderTilemap == null || player == null)
            return;

        var bounds = sourceTilemap.cellBounds;
        float cellHeight = Mathf.Abs(sourceTilemap.layoutGrid.cellSize.y);
        float bufferWorld = verticalBufferTiles * cellHeight;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tile = sourceTilemap.GetTile(cellPos);

                if (tile == null)
                {
                    // ensure collider tile removed for empty visual tiles
                    if (colliderTilemap.GetTile(cellPos) != null)
                        colliderTilemap.SetTile(cellPos, null);
                    continue;
                }

                Vector3 cellCenterWorld = sourceTilemap.GetCellCenterWorld(cellPos);

                // If player is above the tile (with optional buffer) enable collider tile, otherwise remove it
                if (player.position.y > cellCenterWorld.y + bufferWorld)
                {
                    if (colliderTilemap.GetTile(cellPos) == null)
                        colliderTilemap.SetTile(cellPos, tile);
                }
                else
                {
                    if (colliderTilemap.GetTile(cellPos) != null)
                        colliderTilemap.SetTile(cellPos, null);
                }
            }
        }
    }
}