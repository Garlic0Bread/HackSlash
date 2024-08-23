using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OWL
{
    public class PLayer_UI_Manager : MonoBehaviour
    {
        public static PLayer_UI_Manager Instance;

        [Header("NETWORK JOIN")]
        [SerializeField] bool startGame_As_Client;

        public PLayer_UI_Manager UI_manager
        {
            get { return Instance; }
            set { Instance = value; }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            if (startGame_As_Client)
            {
                //must shut down network as a host(happened during titleScreen) before we start as a client
                startGame_As_Client = false;
                NetworkManager.Singleton.Shutdown();

                //start network as a client
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
