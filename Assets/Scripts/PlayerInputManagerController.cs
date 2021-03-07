using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerController : MonoBehaviour
{
    [SerializeField] private GameObject boundsPrefab;
    [SerializeField] private GameObject shipPrefab;
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

    private void Start()
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

        PlayerInputManager.instance.playerPrefab = shipPrefab;

        CanvasManager.Instance.SetLeftHUD(true);
        CanvasManager.Instance.SetRightHUD(false);
    }

    public void OnPlayerJoined(PlayerInput pi)
    {
        if (pi.playerIndex == 0)
        {
            allCam.GetComponent<CinemachineVirtualCamera>().Follow = pi.transform;
            CameraMiddleMan.Instance.AddPlayer(pi.gameObject);
            pi.GetComponent<Spaceboy>().SetIdx(0);
        }
        else if (pi.playerIndex == 1)
        {
            CanvasManager.Instance.SetRightHUD(true);

            leftCam.GetComponent<CinemachineVirtualCamera>().Follow = allCam.GetComponent<CinemachineVirtualCamera>().Follow;
            rightCam.GetComponent<CinemachineVirtualCamera>().Follow = pi.transform;
            CameraMiddleMan.Instance.AddPlayer(pi.gameObject);
            pi.GetComponent<Spaceboy>().SetIdx(1);

            leftBrain.SetActive(true);
            rightBrain.SetActive(true);
            allBrain.SetActive(false);

            leftCam.SetActive(true);
            rightCam.SetActive(true);
            allCam.SetActive(false);

            PlayerInputManager.instance.DisableJoining();
        }
    }
}
