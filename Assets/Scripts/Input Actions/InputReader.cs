using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [field: SerializeField] private bool CanAttack = true;
    [field: SerializeField] private bool CanInteract = true;
    [HideInInspector] public Vector2 MovementAxis;
    [HideInInspector] public Vector2 RotationAxis;
    [HideInInspector] public bool Attack = false;
    [HideInInspector] public bool Interact = false;
    [HideInInspector] public bool ToggleWallet = false;

    public UnityEvent ChangedInputToMouseAndKeyboard;
    public UnityEvent ChangedInputToGamepad;

    private bool isMouseAndKeyboard = true;
    private bool oldInput = true;



    private PlayerActions controls;

    private void Awake()
    {
        controls = new PlayerActions();
        controls.Gameplay.Enable();

        controls.Gameplay.Movement.performed += ctx => OnMove(ctx);
        controls.Gameplay.Rotate.performed += ctx => OnRotate(ctx);
        controls.Gameplay.Attack.performed += ctx => OnAttack(ctx);
        controls.Gameplay.Attack.canceled += ctx => OnAttackEnded(ctx);
        controls.Gameplay.Interact.performed += ctx => OnInteract(ctx);
        controls.Gameplay.Interact.canceled += ctx => OnInteractEnded(ctx);

        controls.Gameplay.ToggleWallet.performed += ctx => OnWalletToggled(ctx);
    }

    private void OnWalletToggled(InputAction.CallbackContext ctx)
    {
        ToggleWallet = !ToggleWallet;
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (CanAttack)
        {
            Attack = true;
        }
    }
    private void OnAttackEnded(InputAction.CallbackContext ctx)
    {
        Attack = false;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (CanInteract)
        {
            Interact = true;
        }
    }
    private void OnInteractEnded(InputAction.CallbackContext ctx)
    {
        Interact = false;
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        MovementAxis = ctx.ReadValue<Vector2>();
        GetDeviceNew(ctx);
    }
    private void OnRotate(InputAction.CallbackContext ctx)
    {
        RotationAxis = ctx.ReadValue<Vector2>();
        GetDeviceNew(ctx);
    }


    private void GetDeviceNew(InputAction.CallbackContext ctx)
    {
        oldInput = isMouseAndKeyboard;

        if (ctx.control.device is Keyboard || ctx.control.device is Mouse) isMouseAndKeyboard = true;
        else isMouseAndKeyboard = false;

        if (oldInput != isMouseAndKeyboard && isMouseAndKeyboard) ChangedInputToMouseAndKeyboard.Invoke();
        else if (oldInput != isMouseAndKeyboard && !isMouseAndKeyboard) ChangedInputToGamepad.Invoke();
    }

}