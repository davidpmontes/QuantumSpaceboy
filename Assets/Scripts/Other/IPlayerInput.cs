using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerInput
{
    float GetThrusterInput();
    Vector2 GetLeftStickInput();
    bool PrimaryFireInput();
    bool SecondaryFireInput();
    void RegisterTowStartEvent(System.Action<InputAction.CallbackContext> d);
    void RegisterStartEvent(System.Action<InputAction.CallbackContext> d);
    void RegisterUpEvent(System.Action<InputAction.CallbackContext> d);
    void RegisterDownEvent(System.Action<InputAction.CallbackContext> d);
    void RegisterLeftEvent(System.Action<InputAction.CallbackContext> d);
    void RegisterRightEvent(System.Action<InputAction.CallbackContext> d);
}