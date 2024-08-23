using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

//functions as the central point for the multiple scripts that player will have
//i.e., combat scipt, animation manager, locomotion manager. When one script needs to call another script instead of
//having a reference to the other script, it can call the method in Character_Manager
namespace OWL
{
    public class Character_Manager : NetworkBehaviour
    {
        [HideInInspector] public CharacterController characterController;//making it here since both player & ai will have one
        [HideInInspector] public Animator animator;

        Character_NetworkManager char_NetworkManager;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);//allow the player to survive the scene change
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            char_NetworkManager = GetComponent<Character_NetworkManager>();
        }
        protected virtual void Update()
        {
            //allows for your gameobject's pos to reflect on the hosts/clients game world

            if (IsOwner)//if you're owner of the network object/Are you host? assign netwok pos to OUR transform
            {
                char_NetworkManager.networkPosition.Value = transform.position;
                char_NetworkManager.networkRotation.Value = transform.rotation;
            }
            else//if not then assign our gameobject's pos to the gameobject that represents me in the host's world 
            {
                transform.position = Vector3.SmoothDamp(transform.position, //position
                    char_NetworkManager.networkPosition.Value, ref char_NetworkManager.networkPositionVelocity, 
                    char_NetworkManager.networkPositionSmoothTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, char_NetworkManager.networkRotation.Value, 
                    char_NetworkManager.networkRotationSmoothTime);//rotation
            }
        }

        protected virtual void LateUpdate()
        {

        }
    }
}

