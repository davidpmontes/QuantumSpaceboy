using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }

    [SerializeField] private GameObject shipPrefab;

    [SerializeField] private GameObject boundsPrefab;
    [SerializeField] private InputDevice inputDevice;

    [SerializeField] private GameObject camLeftBrainPrefab;
    [SerializeField] private GameObject camRightBrainPrefab;
    [SerializeField] private GameObject camAllBrainPrefab;

    [SerializeField] private GameObject camLeftPrefab;
    [SerializeField] private GameObject camRightPrefab;
    [SerializeField] private GameObject camAllPrefab;

    private GameObject leftBrain;
    private GameObject rightBrain;
    private GameObject allBrain;

    private GameObject leftCam;
    private GameObject rightCam;
    private GameObject allCam;

    private void Awake()
    {
        Instance = this;
    }

    public void StartPlayer1()
    {
        Destroy(GameObject.Find("Camera"));

        var bounds = Instantiate(boundsPrefab);

        leftBrain = Instantiate(camLeftBrainPrefab);
        rightBrain = Instantiate(camRightBrainPrefab);
        allBrain = Instantiate(camAllBrainPrefab);

        leftCam = Instantiate(camLeftPrefab);
        rightCam = Instantiate(camRightPrefab);
        allCam = Instantiate(camAllPrefab);

        leftBrain.SetActive(false);
        rightBrain.SetActive(false);
        allBrain.SetActive(true);

        leftCam.SetActive(false);
        rightCam.SetActive(false);
        allCam.SetActive(true);

        leftCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = bounds.GetComponent<Collider2D>();
        rightCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = bounds.GetComponent<Collider2D>();
        allCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = bounds.GetComponent<Collider2D>();

        GameplayCanvasManager.Instance.SetLeftHUD(true);
        GameplayCanvasManager.Instance.SetRightHUD(false);

        var player1 = Instantiate(shipPrefab);
        allCam.GetComponent<CinemachineVirtualCamera>().Follow = player1.transform;
        CameraMiddleMan.Instance.AddPlayer(player1);
        player1.GetComponent<Spaceboy>().SetPlayerInput(PlayerInputControllerManager.Instance.PlayerInput1.GetComponent<IPlayerInput>());
        player1.GetComponent<Spaceboy>().SetIdx(0);
    }
}
