using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class _InputManager1 : MonoBehaviour
{
   [HideInInspector] public PlayerControls playerControls;
    public static _InputManager instance;
    public static PlayerInput playerInput;
    public static Vector2 Movement;

    private InputAction moveAction;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Movement_P2"];
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
    }
}
