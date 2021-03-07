using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] private Tile[] tiles;
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        Random.InitState(1);

        var minX = tilemap.cellBounds.min.x;
        var maxX = tilemap.cellBounds.min.x + tilemap.cellBounds.size.x;
        var minY = tilemap.cellBounds.min.y;
        var maxY = tilemap.cellBounds.min.y + tilemap.cellBounds.size.y;

        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                int rnd = Random.Range(0, tiles.Length);

                tilemap.SetTile(new Vector3Int(x, y, 0), tiles[rnd]);
            }
        }
    }
}