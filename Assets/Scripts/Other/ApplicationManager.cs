using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager Instance { get; private set; }

    [SerializeField] private GameObject objectPoolPrefab = default;
    [SerializeField] private GameObject playerInputManagerControllerPrefab = default;
    [SerializeField] private GameObject menuManagerPrefab = default;

    [SerializeField] private GameObject gameplayManagerPrefab = default;
    [SerializeField] private GameObject tileMapManagerPrefab = default;
    [SerializeField] private GameObject worldObjectsPrefab = default;


    private void Awake()
    {
        Instance = this;

        Instantiate(objectPoolPrefab).GetComponent<ObjectPool>().Init();
        Instantiate(playerInputManagerControllerPrefab).GetComponent<PlayerInputControllerManager>().Init();
        Instantiate(menuManagerPrefab);
    }

    public void StartPlaying()
    {
        Instantiate(tileMapManagerPrefab);
        var wo = Instantiate(worldObjectsPrefab);
        wo.GetComponent<WorldObjectsGroupManager>().InitBattle1();
        Instantiate(gameplayManagerPrefab);
        GamePlayManager.Instance.StartPlayer1();
    }

    public void Pause()
    {

    }
}
