﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Conductor : MonoBehaviour
{
    // Adapted from https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php
    // This script should be attached to the GameObject that is playing the music
    
    // Public variables that will need to be accessed by other scripts
    // The song's BPM
    public float songBpm;
    
    // The offset to the first beat of the song in seconds
    public float firstBeatOffset;
    
    public AudioSource musicSource;
    
    [Header("DO NOT MANUALLY SET")]
    // Calculated from BPM
    public float secPerBeat;
    
    // Current song position in seconds
    public float songPosition;
    
    // Current song position in beats
    // Note: Starts at 0
    public float songPositionInBeats;
    
    // Used to trigger OnBeat Event
    private float lastBeatPosition;
    
    // Number of seconds since the song started
    public float dspSongTime;

    public delegate void ComposerAction();
    public static event ComposerAction OnBeat;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float) AudioSettings.dspTime;
        musicSource.Play();

        lastBeatPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset) / secPerBeat;
        
        // May or may not be needed depending on how this is ultimately set up
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats % 1f - lastBeatPosition < 0)
        {
            if (OnBeat != null)
            {
                OnBeat();
            }
        }
        lastBeatPosition = songPositionInBeats % 1f;
    }
}