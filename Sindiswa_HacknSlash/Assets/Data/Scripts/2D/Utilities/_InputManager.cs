using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

namespace OWL
{
    public class _InputManager : MonoBehaviour
    {
        public static PlayerInput playerInput;
        public static Vector2 Movement;

        public static bool runIsHeld;
        public static bool jumpIsHeld;
        public static bool jumpWasPressed;
        public static bool jumpWasReleased;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction runAction;

        private void Awake()
        {
            playerInput = GetComponent <PlayerInput>();

            moveAction = playerInput.actions["Movement"];
            jumpAction = playerInput.actions["Jump"];
            runAction = playerInput.actions["Dash"];
        }

        private void Update()
        {
            Movement = moveAction.ReadValue<Vector2>();

            runIsHeld = runAction.IsPressed();
            jumpIsHeld = jumpAction.IsPressed();
            jumpWasPressed = jumpAction.WasPressedThisFrame();
            jumpWasReleased = jumpAction.WasReleasedThisFrame();
        }
    }
}

