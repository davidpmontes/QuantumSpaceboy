using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    public static TileMapManager Instance { get; private set; }

    public void Init()
    {
        Instance = this;
    }
}
