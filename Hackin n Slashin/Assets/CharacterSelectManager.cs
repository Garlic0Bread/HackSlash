using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public GetCharacter getCharacterScript;
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
    public bool Player1Turn;
    public bool Player1HasSelected;
    public bool Player2Turn;
    public bool Player2HasSelected;


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
        }
    }

    void Start()
    {
        Player1Turn = true;
        Player2Turn = false;
        Player1HasSelected = false;
        Player2HasSelected = false;
    }

    public void SwitchP1toP2()
    {
        if (Player1Turn == false)
        {
            Player2Turn = true;
        }
    }

    public void CharactersSelectedCheck()
    {
        if (Player1HasSelected && Player2HasSelected)
        {
            StartCoroutine(ChangeScene());
        }
    }

    public void PlayerIMGSelect()
    {
        
    }

    public void CharacterHasBeenSelected()
    {
        if (Player1Turn == true)
        {
            Debug.Log("This script is working");
            Char1_IMG.SetActive(true);
            Player1Turn = false;
            Player1HasSelected = true;
            Player2Turn = true;
            //getCharacterScript.HasBeenSelected();
        }
        if (Player2Turn == true)
        {
            Player2Turn = false;
            Player2HasSelected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CharactersSelectedCheck();
    }

    IEnumerator ChangeScene()
    {
        while (true) 
        {
            yield return new WaitForSeconds(20f);
            SceneManager.LoadScene("Matts_Test_Scene");            
        }
    }
}



