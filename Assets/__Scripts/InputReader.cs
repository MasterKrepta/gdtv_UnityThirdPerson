using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions

{

    public event Action OnJumpEvent;
    public event Action OnDodgeEvent;

    private Controls _controls;
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);

        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        OnJumpEvent?.Invoke();    
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        OnDodgeEvent?.Invoke();
    }
}
