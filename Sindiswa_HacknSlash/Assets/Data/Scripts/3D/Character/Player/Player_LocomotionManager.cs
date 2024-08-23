using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OWL
{
    public class Player_LocomotionManager : Character_LocomotionManager//receive base stats
    {
        //values will be received from inputManager
        Player_Manager player;

        public float moveAmount;
        public float verticalMovement;
        public float horizontalMovement;

        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float walkingSpeed = 3;
        [SerializeField] float runningSpeed = 6;
        [SerializeField] float rotationSpeed = 15;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<Player_Manager>();
        }

        public void HandleAllMovements()
        {
            HandleGroundMovement();
            HandleAirMovement();
            HandleRotation();
        }

        private void Get_VerticalAndHorizontal_Inputs()
        {
            verticalMovement = Player_InputManager.instance.verticalInput;
            horizontalMovement = Player_InputManager.instance.horizontalInput;

            //clamp movement for animation
        }
        private void HandleGroundMovement()
        {//move direction is based on camera facing perspective & inputs
            Get_VerticalAndHorizontal_Inputs();
            moveDirection = Player_Camera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + Player_Camera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0; ;

            if(Player_InputManager.instance.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                //move at running speed
            }
            else if(Player_InputManager.instance.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                //move at walking speed
            }
        }
        private void HandleAirMovement()
        {

        }
        private void HandleRotation()
        {
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = Player_Camera.instance.camObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + Player_Camera.instance.camObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;
            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotatio = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotatio;
        }

    }
}

