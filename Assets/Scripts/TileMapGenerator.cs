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

        for (int x = -31; x < 25; x++)
        {
            for (int y = -21; y < 11; y++)
            {
                int rnd = Random.Range(0, tiles.Length);

                tilemap.SetTile(new Vector3Int(x, y, 0), tiles[rnd]);
            }
        }
    }
}
