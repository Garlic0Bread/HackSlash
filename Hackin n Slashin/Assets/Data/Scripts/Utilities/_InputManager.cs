using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class _InputManager : MonoBehaviour
{
    [HideInInspector] public PlayerControls playerControls;
    public static _InputManager instance;
    public static PlayerInput playerInput;
    public static Vector2 Movement;

    public static bool runIsHeld;
    public static bool isAttacking;
    public static bool jumpIsHeld;
    public static bool jumpWasPressed;
    public static bool jumpWasReleased;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction attackingAction;
    public string player_MovementName;

    //private InputAction attack;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        attackingAction = playerInput.actions["Attack"];
        moveAction = playerInput.actions[player_MovementName];
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

