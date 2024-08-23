using OWL;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace OWL
{
    public class Player_Camera : MonoBehaviour
    {
        [SerializeField] Transform camPivotTransform;
        public static Player_Camera instance;
        public Player_Manager player;
        public Camera camObject;

        //changes how cam performs. We change these values to tweak the cam
        [Header("Camera Settings")]
        private float cameraSmoothSpeed; //the bigger the value the longer it takes for cam to catch up tp u
        [SerializeField] private float UpAndDown_RotationSpeed = 220;
        [SerializeField] private float LeftAndRight_RotationSpeed = 220;
        [SerializeField] float minimumPivot = -20; //lowest point player can look down
        [SerializeField] float maximumPivot = 50; //highest point player can look down
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;

        //these just represent the cam values
        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; //moves camera object to the position of what its colliding with
        [SerializeField] private float LeftAndRight_LookAngle;
        [SerializeField] private float UpAndDown_LookAngle;
        private float cameraZposition;
        private float targetCamera_Z_Position;

        public Player_Camera UI_manager
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
            cameraZposition = camObject.transform.localScale.z;
        }

        public void Handle_AllCamera_Actions()
        {//follow player. Rotate around player. Collide with objects
            if(player != null)
            {
                Follow_Target();
                //HandleRotations();
                //HandleCameraCollisions();
            }
        }

        private void Follow_Target()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, 
                ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime );
            transform.position = targetCameraPosition;
        }
        private void HandleRotations()
        {//1)hand left right rotation(playerCamera in project) and up down rotation(camPivot in project)
            //2)if locked on target, force rotation towards target

            //normal rotations. 1) rotate left right based off of the CameraInputs, i.e., horizontal movement of the mouse
            LeftAndRight_LookAngle += (Player_InputManager.instance.cameraHorizontalInput * LeftAndRight_RotationSpeed) * Time.deltaTime;
            //2) rotate up n down based off of the CameraInputs, i.e., vertical movement of the mouse
            UpAndDown_LookAngle -= (Player_InputManager.instance.cameraVerticalInput * UpAndDown_RotationSpeed) * Time.deltaTime;
            UpAndDown_LookAngle = Mathf.Clamp(UpAndDown_LookAngle, minimumPivot, maximumPivot);//clamp up and down look angle 

            Vector3 camRotation = Vector3.zero;
            Quaternion targetRotation;
            //actual camera is never moved
            camRotation.y = LeftAndRight_LookAngle;
            targetRotation = Quaternion.Euler(camRotation);
            transform.rotation = targetRotation;//gameObj handles left right rotation

            camRotation = Vector3.zero;
            camRotation.x = UpAndDown_LookAngle;
            targetRotation = Quaternion.Euler(camRotation);
            camPivotTransform.localRotation = targetRotation;//pivot gameObj handles up down rotations
        }
        private void HandleCameraCollisions()
        {
            targetCamera_Z_Position = cameraZposition;

            RaycastHit hit;
            Vector3 direction = camObject.transform.position - camPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(camPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCamera_Z_Position), collideWithLayers))
            {
                float distancefromObject = Vector3.Distance(camPivotTransform.position, hit.point);
                targetCamera_Z_Position = -(distancefromObject - cameraCollisionRadius);
            }

            if(Mathf.Abs(targetCamera_Z_Position) < cameraCollisionRadius)
            {
                targetCamera_Z_Position = -cameraCollisionRadius;
            }
            cameraObjectPosition.z = Mathf.Lerp(camObject.transform.localPosition.z, targetCamera_Z_Position, cameraCollisionRadius);
            camObject.transform.localPosition = cameraObjectPosition;
        }
    }
}

