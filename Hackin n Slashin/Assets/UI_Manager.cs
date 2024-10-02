using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class UI_Manager : MonoBehaviour
{
    public TextMeshPro scoreCounter;
    public AudioSource_Manager audioSourceManager;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource_Manager audioSource_Manager = GetComponent<AudioSource_Manager>();
    }

    public float PlayerScore()
    {
        score = 0;
        if(audioSourceManager.isOnBeat)
        {
            return score;
        }
        return score;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
