using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource_Manager AS_manager;
    public GameObject EndGameUI;
    public float playerComboStat;
    public bool isGameOver;
    //public bool isRound1;
    //public bool isRound2;

    // Start is called before the first frame update
    void Start()
    {
        EndGameUI.SetActive(false);
        AS_manager = GetComponent<AudioSource_Manager>();
        playerComboStat = 0f;
    }

    public void updateComboCounter()
    {
        if (Input.anyKeyDown && AS_manager.isOnBeat == true)
        {
            playerComboStat++;
        }
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            EndGameUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isGameOver)
        {
        ReturnHome();
        }
        
    }
}
