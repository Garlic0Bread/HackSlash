using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OWL
{
    public class Player_Manager : Character_Manager//inherits everything in the char manager
    {
        [HideInInspector] public Player_AnimatorManager playerAnim_Manager;
        [HideInInspector] public Player_LocomotionManager player_LocomotionManager;


        protected override void Awake()
        {
            //this mean we inherit our base stats, and allows us to create our own as a unique character
            base.Awake();

            player_LocomotionManager = GetComponent<Player_LocomotionManager>();
            playerAnim_Manager = GetComponent<Player_AnimatorManager>();
        }
        protected override void Update()
        {
            base.Update();//inherit base movement stats
            if (!IsOwner)//do not control this chaacter if yoy're not its owner (network stuff)
            {
                return;
            }
            player_LocomotionManager.HandleAllMovements();//handle all of our movement
        }
        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;

            base.LateUpdate();
            Player_Camera.instance.Handle_AllCamera_Actions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)//if this is the player object owned by this local-client
            {
                Player_Camera.instance.player = this;
                Player_InputManager.instance.player = this;
            }
        }
    }
}

