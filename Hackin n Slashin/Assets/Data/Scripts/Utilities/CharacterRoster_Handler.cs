using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CharacterRoster_Handler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject playerOne_Description;
    [SerializeField] private GameObject playerTwo_Description;
    [SerializeField] private GameObject playerOne_Roster;
    [SerializeField] private GameObject playerTwo_Roster;
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private GameObject avatar_Icon;
    bool playerTwo_turn;

    private void Start()
    {
        playerTwo_turn = false;
    }
    private void Update()
    {
        if(playerTwo_turn == false)
        {
            print("player ones turn");
        }
        else if (playerTwo_turn == true)
        {
            print("player two's turn");

            playerTwo_Roster.SetActive(true);
            playerOne_Roster.SetActive(false);

            playerTwo_Description.SetActive(true);
            playerOne_Description.SetActive(false);
        }
    }
    public void Character_Chosen()
    {
        playerTwo_turn = true;
        avatar_Icon.SetActive(true);
    }

    private void Reset()
    {
        playerTwo_turn = false;
        playerOne_Roster.SetActive(true);
        playerTwo_Roster.SetActive(false);
        playerOne_Description.SetActive(true);
        playerTwo_Description.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);
    }
}
