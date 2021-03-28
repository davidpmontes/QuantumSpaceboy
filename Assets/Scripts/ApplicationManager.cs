using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager Instance { get; private set; }

    [SerializeField] private GameObject objectPoolPrefab = default;
    [SerializeField] private GameObject playerInputManagerControllerPrefab = default;
    [SerializeField] private GameObject menuCanvasManager = default;
    [SerializeField] private GameObject gameplayCanvasManagerPrefab = default;
    [SerializeField] private GameObject gameplayManagerPrefab = default;
    [SerializeField] private GameObject mainMenuManagerPrefab = default;
    [SerializeField] private GameObject tileMapManagerPrefab = default;
    [SerializeField] private GameObject worldObjectsPrefab = default;


    private void Awake()
    {
        Instance = this;

        Instantiate(objectPoolPrefab).GetComponent<ObjectPool>().Init();
        Instantiate(menuCanvasManager).GetComponent<MenuCanvasManager>().Init();
        Instantiate(mainMenuManagerPrefab).GetComponent<MainMenuManager>().Init();
        Instantiate(playerInputManagerControllerPrefab);
    }

    public void StartPlaying()
    {
        MainMenuManager.Instance.enabled = false;
        MenuCanvasManager.Instance.SetCanvasVisibility(false);
        Instantiate(tileMapManagerPrefab);
        Instantiate(worldObjectsPrefab);
        Instantiate(gameplayCanvasManagerPrefab);
        Instantiate(gameplayManagerPrefab);
        GamePlayManager.Instance.StartPlayer1();
    }

    public void Pause()
    {

    }
}
