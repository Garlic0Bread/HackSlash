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
        public GameObject player1;
        public GameObject player2;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction runAction;
        private InputAction attack;

        private void Awake()
        {
            /*if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }*/
            player1 = GameObject.FindGameObjectWithTag("Player1");
            player2 = GameObject.FindGameObjectWithTag("Player2");
            

            playerControls = new PlayerControls();
            if (player1)
            {
                playerInput = player1.GetComponent<PlayerInput>();
                playerInput.SwitchCurrentControlScheme("Keyboard", Keyboard.current, Mouse.current);
            }
            else if(player2)
            {
                playerInput = player2.GetComponent<PlayerInput>();
                playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            }

            moveAction = playerInput.actions["Movement"];
            jumpAction = playerInput.actions["Jump"];
            runAction = playerInput.actions["Dash"];
            attack = playerInput.actions["Attack"];
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
            isAttacking = attack.IsPressed();
            jumpWasPressed = jumpAction.WasPressedThisFrame();
            jumpWasReleased = jumpAction.WasReleasedThisFrame();
        }
    }
}

