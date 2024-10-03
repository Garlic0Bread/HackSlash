using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public AudioSource_Manager AS_manager;
    public float playerComboStat;
    //public bool isRound1;
    //public bool isRound2;

    // Start is called before the first frame update
    void Start()
    {
        AS_manager = GetComponent<AudioSource_Manager>();
        playerComboStat = 0f;    
    }

    public void updateComboCounter()
    {
        if(Input.anyKeyDown && AS_manager.isOnBeat == true)
        {
            playerComboStat++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
