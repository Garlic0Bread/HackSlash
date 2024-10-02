using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class _InputManager1 : MonoBehaviour
{
   [HideInInspector] public PlayerControls playerControls;
    public static _InputManager1 instance;
    public static PlayerInput playerInput;
    public static Vector2 Movement;

    public static bool jumpWasReleased;
    public static bool jumpWasPressed;
    public static bool isAttacking;
    public static bool jumpIsHeld;
    public static bool runIsHeld;

    private InputAction attackingAction;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();

        attackingAction = playerInput.actions["Attack"];
        moveAction = playerInput.actions["Movement_P2"];
        jumpAction = playerInput.actions["Jump"];
        runAction = playerInput.actions["Dash"];
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls?.Disable();
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        runIsHeld = runAction.IsPressed();
        jumpIsHeld = jumpAction.IsPressed();
        isAttacking = attackingAction.IsPressed();
        jumpWasPressed = jumpAction.WasPressedThisFrame();
        jumpWasReleased = jumpAction.WasReleasedThisFrame();
    }
}
