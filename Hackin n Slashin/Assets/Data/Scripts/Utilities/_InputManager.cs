using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

namespace OWL
{
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
        //private InputAction attack;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            playerControls = new PlayerControls();
            playerInput = GetComponent <PlayerInput>();

            moveAction = playerInput.actions["Movement"];
            jumpAction = playerInput.actions["Jump"];
            runAction = playerInput.actions["Dash"];
            //attack = playerInput.actions["Attack2"];
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
            //isAttacking = attack.WasPressedThisFrame();
            jumpWasPressed = jumpAction.WasPressedThisFrame();
            jumpWasReleased = jumpAction.WasReleasedThisFrame();
        }
    }
}

