using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OWL
{
    public class TitleScreen_Manager : MonoBehaviour
    {
        public void StartNetwork()//allows the player to function is both the host and the server, this begins the network to allow clients to connect to the server
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            StartCoroutine(Wordl_SaveGame_Manager.instance.LoadNewGame());
        }
    }
}

