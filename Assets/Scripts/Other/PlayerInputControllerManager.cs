using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControllerManager : MonoBehaviour
{
    public static PlayerInputControllerManager Instance { get; private set; }
    private PlayerInput playerInput1;
    private PlayerInput playerInput2;

    public void Init()
    {
        Instance = this;
    }

    public PlayerInput SubscribeToPlayer1()
    {
        return playerInput1;
    }    

    public void OnPlayerJoined(PlayerInput pi)
    {
        Debug.Log(pi.playerIndex);

        if (pi.playerIndex == 0)
        {
            pi.GetComponent<QSBPlayerInput>().Init();
            //MainMenuManager.Instance.SetPlayerInput(pi.GetComponent<IPlayerInput>());
            playerInput1 = pi;
        }
        else if (pi.playerIndex == 1)
        {
            playerInput2 = pi;
        //    Debug.Log(1);
        //    GameplayCanvasManager.Instance.SetRightHUD(true);

        //    leftCam.GetComponent<CinemachineVirtualCamera>().Follow = allCam.GetComponent<CinemachineVirtualCamera>().Follow;
        //    rightCam.GetComponent<CinemachineVirtualCamera>().Follow = pi.transform;
        //    CameraMiddleMan.Instance.AddPlayer(pi.gameObject);
        //    pi.GetComponent<Spaceboy>().SetPlayerInput(pi);
        //    pi.GetComponent<Spaceboy>().SetIdx(1);

        //    leftBrain.SetActive(true);
        //    rightBrain.SetActive(true);
        //    allBrain.SetActive(false);

        //    leftCam.SetActive(true);
        //    rightCam.SetActive(true);
        //    allCam.SetActive(false);

            PlayerInputManager.instance.DisableJoining();
        }
    }
}
