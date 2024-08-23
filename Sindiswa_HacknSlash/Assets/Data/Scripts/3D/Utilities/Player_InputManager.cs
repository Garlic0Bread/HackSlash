using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace OWL
{
    public class Player_InputManager : MonoBehaviour
    {
        //Goals: 1. Read vaues of joystick/WASD. 2. Move character based on those values 3. Stop avatar from moving at menu scene
        public static Player_InputManager instance;
        public Player_Manager player;
        PlayerControls playerControls;

        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;
        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        

        public Player_InputManager player_InputeManager
        {
            get { return instance; }
            set { instance = value; }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            //when scene changes run this logic to stop avatar from being moved when in player is in mainMenu
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;//only turn on when scene changes  
        }
        private void Update()
        {
            HandleMovementInput();
            HandleCameraInput();
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            //if new scene is game world, enable player controls script not game object,but the instance script itself
            if(newScene.buildIndex == Wordl_SaveGame_Manager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else //if oldscene is main menu, disable player script to move
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();//find playercontrols if its null
                //when Movement action of PlayerMovement actionmap is performed, assign its values to movememtInput/camInput
                //we can now use movementInput/camInput to move the character
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.CamMovement.performed += i => cameraInput = i.ReadValue<Vector2>();
            }
            playerControls.Enable();
        }
        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }
        private void OnApplicationFocus(bool focus)
        {//if game is minimized stop receiving inputs from player/stop moving avatar
            if(enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            //clamp01 makes sure value is between 0 & 1. We use this to combine the x and y inputs
            //we created to the varibale moveAmount
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));//abs creates and absolute value so there are no signs in front of the number

            //clamp values so they are 0, 0.5 or 1
            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if( moveAmount > 0.5 && moveAmount <= 1 )
            {
                moveAmount = 1;
            }
            
            player.playerAnim_Manager.UpdateAnimator_MovementParameters(0, moveAmount);
        } 
        private void HandleCameraInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;

            /*
             if (Input.GetKey(KeyCode.Mouse2))
            {
                cameraVerticalInput = cameraInput.y;
                cameraHorizontalInput = cameraInput.x;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                cameraHorizontalInput = 0;
                cameraVerticalInput = 0;
            }
             */

        }
    }
}

