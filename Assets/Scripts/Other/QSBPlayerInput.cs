﻿using UnityEngine;
using UnityEngine.InputSystem;

public class QSBPlayerInput : MonoBehaviour, IPlayerInput
{
    private PlayerInput playerInput;    

    public void Init()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void RegisterTowStartEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Tow"].started += function;
    }

    public void RegisterStartEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Start"].started += function;
    }

    public void RegisterUpEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Up"].performed += function;
    }

    public void RegisterDownEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Down"].performed += function;
    }

    public void RegisterLeftEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Left"].performed += function;
    }

    public void RegisterRightEvent(System.Action<InputAction.CallbackContext> function)
    {
        playerInput.actions["Right"].performed += function;
    }

    public float GetThrusterInput()
    {
        return playerInput.actions["Thruster"].ReadValue<float>();
    }

    public Vector2 GetLeftStickInput()
    {
        return playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public bool PrimaryFireInput()
    {
        return playerInput.actions["Fire"].ReadValue<float>() > 0;
    }

    public bool SecondaryFireInput()
    {
        return playerInput.actions["Secondary"].ReadValue<float>() > 0;
    }
}
