using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource_Manager : MonoBehaviour
{
    [Header("Audio Handling")]
    [SerializeField] private float currentAudioTime;
    [SerializeField] private float timeWindow = 5f; // 5-second window
    [SerializeField] private float targetTime = 20f; // 00:20 in seconds
    [SerializeField] private AudioSource audioSource; // Your music track
    public bool isOnBeat;

    void Start()
    {
        audioSource.Play();
    }

    void Update()
    {
        currentAudioTime = audioSource.time;
        if (currentAudioTime >= targetTime - timeWindow && currentAudioTime <= targetTime + timeWindow)
        {// // Player hits SP attack button within song's keyMoment timing-window, apply bonus damage
            isOnBeat = true;
        }
        else
            isOnBeat = false;
    }
}
