using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    private MENU_STATE menuState;
    private bool inputEnabled = false;

    private enum MENU_STATE
    {
        TITLE_SCREEN,
        START_SCREEN,
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        MenuCanvasManager.Instance.SetToBlack();
        StartCoroutine(SetNewMenuState(MENU_STATE.TITLE_SCREEN));
    }

    public void SetPlayerInput(IPlayerInput playerInput)
    {
        playerInput.RegisterStartEvent(ProcessStartButton);
    }

    private IEnumerator SetNewMenuState(MENU_STATE newState)
    {
        if (newState == MENU_STATE.TITLE_SCREEN)
        {
            MenuCanvasManager.Instance.ShowTitleScreen();
            yield return StartCoroutine(MenuCanvasManager.Instance.FadeToTransparentCoroutine(0));
            inputEnabled = true;
        }
        else if (newState == MENU_STATE.START_SCREEN)
        {
            yield return StartCoroutine(MenuCanvasManager.Instance.FadeToBlackCoroutine(1));
            MenuCanvasManager.Instance.ShowStartScreen();
            yield return StartCoroutine(MenuCanvasManager.Instance.FadeToTransparentCoroutine(1));
            inputEnabled = true;
        }

        yield return null;
    }

    private void ProcessStartButton(InputAction.CallbackContext ctx)
    {
        if (!inputEnabled) return;

        if (menuState == MENU_STATE.TITLE_SCREEN)
        {
            inputEnabled = false;
            menuState = MENU_STATE.START_SCREEN;
            StartCoroutine(SetNewMenuState(menuState));
        }
        else if (menuState == MENU_STATE.START_SCREEN)
        {
            ApplicationManager.Instance.StartPlaying();
        }
    }
}
