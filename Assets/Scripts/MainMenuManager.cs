using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private UnityEngine.InputSystem.PlayerInput playerInput;
    private bool startInput;
    private bool isStartInputDownThisFrame;
    private MENU_STATE menuState;
    private bool inputEnabled = false;

    private enum MENU_STATE
    {
        TITLE_SCREEN,
        START_SCREEN,
    }

    private void Awake()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        GameObject.Find("Canvas").GetComponent<MainMenuCanvasManager>().Init();        
    }

    private void Start()
    {
        MainMenuCanvasManager.Instance.SetToBlack();
        StartCoroutine(SetNewMenuState(MENU_STATE.TITLE_SCREEN));
    }

    private void Update()
    {
        GetInput();
        ProcessInputBasedOnMenuState();
    }

    private void GetInput()
    {
        var newStartInput = playerInput.actions["Start"].ReadValue<float>() > 0;
        if (startInput == false && newStartInput == true)
        {
            isStartInputDownThisFrame = true;
        }
        else
        {
            isStartInputDownThisFrame = false;
        }
        startInput = newStartInput;
    }

    private IEnumerator SetNewMenuState(MENU_STATE newState)
    {
        if (newState == MENU_STATE.TITLE_SCREEN)
        {
            MainMenuCanvasManager.Instance.ShowTitleScreen();
            yield return StartCoroutine(MainMenuCanvasManager.Instance.FadeToTransparentCoroutine(0));
            inputEnabled = true;
        }
        else if (newState == MENU_STATE.START_SCREEN)
        {
            yield return StartCoroutine(MainMenuCanvasManager.Instance.FadeToBlackCoroutine(1));
            MainMenuCanvasManager.Instance.ShowStartScreen();
            yield return StartCoroutine(MainMenuCanvasManager.Instance.FadeToTransparentCoroutine(1));
            inputEnabled = true;
        }

        yield return null;
    }

    private void ProcessInputBasedOnMenuState()
    {
        if (!inputEnabled) return;
        if (!isStartInputDownThisFrame) return;

        if (menuState == MENU_STATE.TITLE_SCREEN)
        {
            inputEnabled = false;
            menuState = MENU_STATE.START_SCREEN;
            StartCoroutine(SetNewMenuState(menuState));
        }
        else if (menuState == MENU_STATE.START_SCREEN)
        {
            Debug.Log("Loading");
            SceneManager.LoadScene("Planet1");
        }
    }
}
