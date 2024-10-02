using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float _songBpm;
    public float _secPerBeat;
    public float _songPos;
    public float _songPosInBeats;
    public float _dspSongTime;
    public float _firstBeatOffset;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _secPerBeat = 60f / _songBpm;
        _dspSongTime = (float)AudioSettings.dspTime;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _songPos = (float)(AudioSettings.dspTime - _dspSongTime - _firstBeatOffset);
        _songPosInBeats = _songPos/_secPerBeat;
    }
}
