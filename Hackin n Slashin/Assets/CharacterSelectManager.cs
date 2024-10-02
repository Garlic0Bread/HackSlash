using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectManager : MonoBehaviour
{
    public GameObject Char1_IMG;
    public GameObject Char2_IMG;
    public GameObject Char3_IMG;
    public GameObject Char4_IMG;
    public GameObject Char5_IMG;
    public GameObject Char6_IMG;
    public GameObject Char7_IMG;
    public GameObject P1CharSelectedIMG;
    public GameObject P2CharSelectedIMG;
    public string Player1_Char;
    public string Player2_Char;

    private List<PlayerConfiguration> playerConfigs;

    [SerializeField] private int MaxPlayers = 2;
    public static CharacterSelectManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Debug.Log("Singleton - trying to create another instance of singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerChar(int index, string CharName)
    {
        playerConfigs[index].PlayerName = CharName;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class PlayerConfiguration
{ 
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public string PlayerName { get; set; }
    public bool IsReady { get; set; }
}

